/*
 * Exchange Web Services Managed API
 *
 * Copyright (c) Microsoft Corporation
 * All rights reserved.
 *
 * MIT License
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this
 * software and associated documentation files (the "Software"), to deal in the Software
 * without restriction, including without limitation the rights to use, copy, modify, merge,
 * publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
 * to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
 * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

namespace Microsoft.Exchange.WebServices.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for all responses that can be sent.
    /// </summary>
    /// <typeparam name="TMessage">Type of message.</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ResponseObject<TMessage> : ServiceObject
        where TMessage : EmailMessage
    {
        private Item referenceItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseObject&lt;TMessage&gt;"/> class.
        /// </summary>
        /// <param name="referenceItem">The reference item.</param>
        internal ResponseObject(Item referenceItem)
            : base(referenceItem.Service)
        {
            EwsUtilities.Assert(
                referenceItem != null,
                "ResponseObject.ctor",
                "referenceItem is null");

            referenceItem.ThrowIfThisIsNew();

            this.referenceItem = referenceItem;
        }

        /// <summary>
        /// Internal method to return the schema associated with this type of object.
        /// </summary>
        /// <returns>The schema associated with this type of object.</returns>
        internal override ServiceObjectSchema GetSchema()
        {
            return ResponseObjectSchema.Instance;
        }

        /// <summary>
        /// Loads the specified set of properties on the object.
        /// </summary>
        /// <param name="propertySet">The properties to load.</param>
        internal override Task<ServiceResponseCollection<ServiceResponse>> InternalLoad(PropertySet propertySet, CancellationToken token)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="deleteMode">The deletion mode.</param>
        /// <param name="sendCancellationsMode">Indicates whether meeting cancellation messages should be sent.</param>
        /// <param name="affectedTaskOccurrences">Indicate which occurrence of a recurring task should be deleted.</param>
        internal override Task<ServiceResponseCollection<ServiceResponse>> InternalDelete(
            DeleteMode deleteMode,
            SendCancellationsMode? sendCancellationsMode,
            AffectedTaskOccurrence? affectedTaskOccurrences,
            CancellationToken token)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Create the response object.
        /// </summary>
        /// <param name="destinationFolderId">The destination folder id.</param>
        /// <param name="messageDisposition">The message disposition.</param>
        /// <returns>The list of items returned by EWS.</returns>
        internal Task<List<Item>> InternalCreate(FolderId destinationFolderId, MessageDisposition messageDisposition, CancellationToken token)
        {
            ((ItemId)this.PropertyBag[ResponseObjectSchema.ReferenceItemId]).Assign(this.referenceItem.Id);

            return this.Service.InternalCreateResponseObject(
                this,
                destinationFolderId,
                messageDisposition,
                token);
        }

        /// <summary>
        /// Saves the response in the specified folder. Calling this method results in a call to EWS.
        /// </summary>
        /// <param name="destinationFolderId">The Id of the folder in which to save the response.</param>
        /// <returns>A TMessage that represents the response.</returns>
        public async Task<TMessage> Save(FolderId destinationFolderId, CancellationToken token = default(CancellationToken))
        {
            EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");

            return (await this.InternalCreate(destinationFolderId, MessageDisposition.SaveOnly, token).ConfigureAwait(false))[0] as TMessage;
        }

        /// <summary>
        /// Saves the response in the specified folder. Calling this method results in a call to EWS.
        /// </summary>
        /// <param name="destinationFolderName">The name of the folder in which to save the response.</param>
        /// <returns>A TMessage that represents the response.</returns>
        public async Task<TMessage> Save(WellKnownFolderName destinationFolderName, CancellationToken token = default(CancellationToken))
        {
            return (await this.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SaveOnly, token).ConfigureAwait(false))[0] as TMessage;
        }

        /// <summary>
        /// Saves the response in the Drafts folder. Calling this method results in a call to EWS.
        /// </summary>
        /// <returns>A TMessage that represents the response.</returns>
        public async Task<TMessage> Save(CancellationToken token = default(CancellationToken))
        {
            return (await this.InternalCreate(null, MessageDisposition.SaveOnly, token).ConfigureAwait(false))[0] as TMessage;
        }

        /// <summary>
        /// Sends this response without saving a copy. Calling this method results in a call to EWS.
        /// </summary>
        public System.Threading.Tasks.Task Send(CancellationToken token = default(CancellationToken))
        {
            return this.InternalCreate(null, MessageDisposition.SendOnly, token);
        }

        /// <summary>
        /// Sends this response and saves a copy in the specified folder. Calling this method results in a call to EWS.
        /// </summary>
        /// <param name="destinationFolderId">The Id of the folder in which to save the copy of the message.</param>
        public System.Threading.Tasks.Task SendAndSaveCopy(FolderId destinationFolderId, CancellationToken token = default(CancellationToken))
        {
            EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");

            return this.InternalCreate(destinationFolderId, MessageDisposition.SendAndSaveCopy, token);
        }

        /// <summary>
        /// Sends this response and saves a copy in the specified folder. Calling this method results in a call to EWS.
        /// </summary>
        /// <param name="destinationFolderName">The name of the folder in which to save the copy of the message.</param>
        public System.Threading.Tasks.Task SendAndSaveCopy(WellKnownFolderName destinationFolderName, CancellationToken token = default(CancellationToken))
        {
            return this.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SendAndSaveCopy, token);
        }

        /// <summary>
        /// Sends this response and saves a copy in the Sent Items folder. Calling this method results in a call to EWS.
        /// </summary>
        public System.Threading.Tasks.Task SendAndSaveCopy(CancellationToken token = default(CancellationToken))
        {
            return this.InternalCreate(
                null,
                MessageDisposition.SendAndSaveCopy,
                token);
        }

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether read receipts will be requested from recipients of this response.
        /// </summary>
        public bool IsReadReceiptRequested
        {
            get { return (bool)this.PropertyBag[EmailMessageSchema.IsReadReceiptRequested]; }
            set { this.PropertyBag[EmailMessageSchema.IsReadReceiptRequested] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether delivery receipts should be sent to the sender.
        /// </summary>
        public bool IsDeliveryReceiptRequested
        {
            get { return (bool)this.PropertyBag[EmailMessageSchema.IsDeliveryReceiptRequested]; }
            set { this.PropertyBag[EmailMessageSchema.IsDeliveryReceiptRequested] = value; }
        }

        #endregion
    }
}