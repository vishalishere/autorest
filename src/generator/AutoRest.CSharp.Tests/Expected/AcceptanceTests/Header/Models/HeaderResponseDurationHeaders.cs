// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Fixtures.AcceptanceTestsHeader.Models
{
    using AcceptanceTestsHeader;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    /// Defines headers for responseDuration operation.
    /// </summary>
    public partial class HeaderResponseDurationHeaders
    {
        /// <summary>
        /// Initializes a new instance of the HeaderResponseDurationHeaders
        /// class.
        /// </summary>
        public HeaderResponseDurationHeaders() { }

        /// <summary>
        /// Initializes a new instance of the HeaderResponseDurationHeaders
        /// class.
        /// </summary>
        /// <param name="value">response with header values
        /// "P123DT22H14M12.011S"</param>
        public HeaderResponseDurationHeaders(System.TimeSpan? value = default(System.TimeSpan?))
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets response with header values "P123DT22H14M12.011S"
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "value")]
        public System.TimeSpan? Value { get; set; }

    }
}
