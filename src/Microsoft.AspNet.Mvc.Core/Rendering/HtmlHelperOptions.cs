﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Mvc.Core;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public class HtmlHelperOptions
    {
        private string _idAttributeDotReplacement = "_";
        private Html5DateRenderingMode _html5DateRenderingMode;
        private bool _clientValidationEnabled = true;
        private string _validationMessageElement = "span";
        private string _validationSummaryMessageElement = "span";

        public HtmlHelperOptions()
        {
        }

        /// <summary>
        /// Specifies the name of the anti-forgery token field that is used by the anti-forgery system.
        /// </summary>
        /// <summary>
        /// Set this property to <see cref="Html5DateRenderingMode.Rfc3339"/> to have templated helpers such as
        /// <see cref="Editor"/> and <see cref="IHtmlHelper{TModel}.EditorFor"/> render date and time values as RFC
        /// 3339 compliant strings. By default these helpers render dates and times using the current culture.
        /// </summary>
        public Html5DateRenderingMode Html5DateRenderingMode
        {
            get
            {
                return _html5DateRenderingMode;
            }
            set
            {
                _html5DateRenderingMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the character that replaces periods in the ID attribute of an element.
        /// </summary>
        public string IdAttributeDotReplacement
        {
            get
            {
                return _idAttributeDotReplacement;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        Resources.FormatPropertyOfTypeCannotBeNull(
                            nameof(IdAttributeDotReplacement),
                            typeof(HtmlHelperOptions)));
                }

                _idAttributeDotReplacement = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether client-side validation is enabled.
        /// </summary>
        public bool ClientValidationEnabled
        {
            get
            {
                return _clientValidationEnabled;
            }

            set
            {
                _clientValidationEnabled = value;
            }
        }

        /// <summary>
        /// Element name used to wrap a top-level message generated by <see cref="IHtmlHelper.ValidationMessage"/> and
        /// other overloads.
        /// </summary>
        public string ValidationMessageElement
        {
            get
            {
                return _validationMessageElement;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        Resources.FormatPropertyOfTypeCannotBeNull(
                            nameof(ValidationMessageElement),
                            typeof(HtmlHelperOptions)));
                }

                _validationMessageElement = value;
            }
        }

        /// <summary>
        /// Element name used to wrap a top-level message generated by <see cref="IHtmlHelper.ValidationSummary"/> and
        /// other overloads.
        /// </summary>
        public string ValidationSummaryMessageElement
        {
            get
            {
                return _validationSummaryMessageElement;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        Resources.FormatPropertyOfTypeCannotBeNull(
                            nameof(ValidationSummaryMessageElement),
                            typeof(HtmlHelperOptions)));
                }

                _validationSummaryMessageElement = value;
            }
        }

    }
}
