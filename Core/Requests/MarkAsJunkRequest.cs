//-----------------------------------------------------------------------
// <copyright file="MarkAsJunkRequest.cs" company="Microsoft Corp.">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Definition for MarkAsJunkRequest
    /// </summary>
    internal sealed class MarkAsJunkRequest : MultiResponseServiceRequest<MarkAsJunkResponse>, IJsonSerializable
    {
        private ItemIdWrapperList itemIds = new ItemIdWrapperList();

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkAsJunkRequest"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="errorHandlingMode"> Indicates how errors should be handled.</param>
        internal MarkAsJunkRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
            : base(service, errorHandlingMode)
        {
        }

        /// <summary>
        /// Validate request.
        /// </summary>
        internal override void Validate()
        {
            base.Validate();
            EwsUtilities.ValidateParam(this.ItemIds, "ItemIds");
        }

        /// <summary>
        /// Creates the service response.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="responseIndex">Index of the response.</param>
        /// <returns>Response object.</returns>
        internal override MarkAsJunkResponse CreateServiceResponse(ExchangeService service, int responseIndex)
        {
            return new MarkAsJunkResponse();
        }

        /// <summary>
        /// Gets the name of the XML element.
        /// </summary>
        /// <returns>XML element name,</returns>
        internal override string GetXmlElementName()
        {
            return XmlElementNames.MarkAsJunk;
        }

        /// <summary>
        /// Gets the name of the response XML element.
        /// </summary>
        /// <returns>Xml element name.</returns>
        internal override string GetResponseXmlElementName()
        {
            return XmlElementNames.MarkAsJunkResponse;
        }

        /// <summary>
        /// Gets the name of the response message XML element.
        /// </summary>
        /// <returns>Xml element name.</returns>
        internal override string GetResponseMessageXmlElementName()
        {
            return XmlElementNames.MarkAsJunkResponseMessage;
        }

        /// <summary>
        /// Gets the expected response message count.
        /// </summary>
        /// <returns>Number of items in response.</returns>
        internal override int GetExpectedResponseMessageCount()
        {
            return this.itemIds.Count;
        }

        /// <summary>
        /// Writes attribute.
        /// </summary>
        /// <param name="writer">Xml writer.</param>
        internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
        {
            writer.WriteAttributeValue(XmlAttributeNames.IsJunk, this.IsJunk);
            writer.WriteAttributeValue(XmlAttributeNames.MoveItem, this.MoveItem);
        }

        /// <summary>
        /// Writes XML elements.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
        {
            this.itemIds.WriteToXml(writer, XmlNamespace.Messages, XmlElementNames.ItemIds);
        }

        /// <summary>
        /// Creates a JSON representation of this object.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>
        /// A Json value (either a JsonObject, an array of Json values, or a Json primitive)
        /// </returns>
        object IJsonSerializable.ToJson(ExchangeService service)
        {
            JsonObject jsonRequest = new JsonObject();
            jsonRequest.Add(XmlElementNames.ItemIds, this.ItemIds.InternalToJson(service));
            return jsonRequest;
        }

        /// <summary>
        /// Gets the request version.
        /// </summary>
        /// <returns>Earliest Exchange version in which this request is supported.</returns>
        internal override ExchangeVersion GetMinimumRequiredServerVersion()
        {
            return ExchangeVersion.Exchange2013;
        }

        /// <summary>
        /// Gets the item ids.
        /// </summary>
        /// <value>The item ids.</value>
        internal ItemIdWrapperList ItemIds
        {
            get { return this.itemIds; }
        }

        /// <summary>
        /// Gets or sets the isJunk flag.
        /// If true, add sender to junk email rule
        /// If false,remove sender to junk email rule
        /// </summary>
        /// <value>The IsJunk flag.</value>
        internal bool IsJunk
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the MoveItem flag.
        /// If true, item is moved to junk folder if IsJunk is true. Item is moved to inbox if IsJunk is false.
        /// If false, item is not moved.
        /// </summary>
        /// <value>The MoveItem flag.</value>
        internal bool MoveItem
        {
            get;
            set;
        }
    }
}