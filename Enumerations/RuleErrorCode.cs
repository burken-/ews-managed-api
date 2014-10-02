// ---------------------------------------------------------------------------
// <copyright file="RuleErrorCode.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the RuleErrorCode enumeration.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    /// <summary>
    /// Defines the error codes identifying why a rule failed validation.
    /// </summary>
    public enum RuleErrorCode
    {
        /// <summary>
        /// Active Directory operation failed.
        /// </summary>
        ADOperationFailure,

        /// <summary>
        /// The e-mail account specified in the FromConnectedAccounts predicate
        /// was not found.
        /// </summary>
        ConnectedAccountNotFound,

        /// <summary>
        /// The Rule object in a CreateInboxRuleOperation has an Id. The Ids of new 
        /// rules are generated server side and should not be provided by the client.
        /// </summary>
        CreateWithRuleId,

        /// <summary>
        /// The value is empty. An empty value is not allowed for the property.
        /// </summary>
        EmptyValueFound,

        /// <summary>
        /// There already is a rule with the same priority. 
        /// </summary>
        DuplicatedPriority,

        /// <summary>
        /// There are multiple operations against the same rule. Only one 
        /// operation per rule is allowed.
        /// </summary>
        DuplicatedOperationOnTheSameRule,

        /// <summary>
        /// The folder does not exist in the user's mailbox.
        /// </summary>
        FolderDoesNotExist,

        /// <summary>
        /// The e-mail address is invalid.
        /// </summary>
        InvalidAddress,

        /// <summary>
        /// The date range is invalid.
        /// </summary>
        InvalidDateRange,

        /// <summary>
        /// The folder Id is invalid.
        /// </summary>
        InvalidFolderId,

        /// <summary>
        /// The size range is invalid.
        /// </summary>
        InvalidSizeRange,

        /// <summary>
        /// The value is invalid.
        /// </summary>
        InvalidValue,

        /// <summary>
        /// The message classification was not found.
        /// </summary>
        MessageClassificationNotFound,

        /// <summary>
        /// No action was specified. At least one action must be specified.
        /// </summary>
        MissingAction,

        /// <summary>
        /// The required parameter is missing.
        /// </summary>
        MissingParameter,

        /// <summary>
        /// The range value is missing.
        /// </summary>
        MissingRangeValue,

        /// <summary>
        /// The property cannot be modified.
        /// </summary>
        NotSettable,

        /// <summary>
        /// The recipient does not exist.
        /// </summary>
        RecipientDoesNotExist,

        /// <summary>
        /// The rule was not found.
        /// </summary>
        RuleNotFound,

        /// <summary>
        /// The size is less than zero.
        /// </summary>
        SizeLessThanZero,

        /// <summary>
        /// The string value is too big.
        /// </summary>
        StringValueTooBig,

        /// <summary>
        /// The address is unsupported.
        /// </summary>
        UnsupportedAddress,

        /// <summary>
        /// An unexpected error occured.
        /// </summary>
        UnexpectedError,

        /// <summary>
        /// The rule is not supported.
        /// </summary>
        UnsupportedRule
    }
}