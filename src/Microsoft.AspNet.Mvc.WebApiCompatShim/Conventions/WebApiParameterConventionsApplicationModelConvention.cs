// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Microsoft.AspNet.Mvc.WebApiCompatShim
{
    public class WebApiParameterConventionsApplicationModelConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (IsConventionApplicable(action.Controller))
            {
                var optionalParameters = new HashSet<string>();
                var uriBindingSource = (new FromUriAttribute()).BindingSource;
                foreach (var parameter in action.Parameters)
                {
                    // Some IBindingSourceMetadata attributes like ModelBinder attribute return null 
                    // as their binding source. Special case to ensure we do not ignore them.
                    if (parameter.BindingInfo?.BindingSource != null ||
                        parameter.Attributes.OfType<IBindingSourceMetadata>().Any())
                    {
                        // This has a binding behavior configured, just leave it alone.
                    }
                    else if (ValueProviderResult.CanConvertFromString(parameter.ParameterInfo.ParameterType))
                    {
                        // Simple types are by-default from the URI.
                        parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
                        parameter.BindingInfo.BindingSource = uriBindingSource;
                    }
                    else
                    {
                        // Complex types are by-default from the body.
                        parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
                        parameter.BindingInfo.BindingSource = BindingSource.Body;
                    }

                    // For all non IOptionalBinderMetadata, which are not URL source (like FromQuery etc.) do not
                    // participate in overload selection and hence are added to the hashset so that they can be
                    // ignored in OverloadActionConstraint.
                    var optionalMetadata = parameter.Attributes.OfType<IOptionalBinderMetadata>().SingleOrDefault();
                    if (parameter.ParameterInfo.HasDefaultValue && parameter.BindingInfo.BindingSource == uriBindingSource ||
                        optionalMetadata != null && optionalMetadata.IsOptional ||
                        optionalMetadata == null && parameter.BindingInfo.BindingSource != uriBindingSource)
                    {
                        optionalParameters.Add(parameter.ParameterName);
                    }
                }

                action.Properties.Add("OptionalParameters", optionalParameters);
            }
        }

        private bool IsConventionApplicable(ControllerModel controller)
        {
            return controller.Attributes.OfType<IUseWebApiParameterConventions>().Any();
        }
    }
}