﻿using System;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;

namespace GlobalPayments.Api.Builders {
    /// <summary>
    /// Used to create charges, verifies, etc. for the supported
    /// payment method types.
    /// </summary>
    public class AuthorizationBuilder : TransactionBuilder<Transaction> {
        internal string Alias { get; set; }
        internal AliasAction? AliasAction { get; set; }
        internal bool AllowDuplicates { get; set; }
        internal bool AllowPartialAuth { get; set; }
        internal decimal? Amount { get; set; }
        internal decimal? AuthAmount { get; set; }
        internal InquiryType? BalanceInquiryType { get; set; }
        internal Address BillingAddress { get; set; }
        internal decimal? CashBackAmount { get; set; }
        internal string ClientTransactionId { get; set; }
        internal string Currency { get; set; }
        internal string CustomerId { get; set; }
        internal string CustomerIpAddress { get; set; }
        internal string Cvn { get; set; }
        internal string Description { get; set; }
        internal string DynamicDescriptor { get; set; }
        internal EcommerceInfo EcommerceInfo { get; set; }
        internal decimal? Gratuity { get; set; }
        internal decimal? ConvenienceAmt { get; set; }
        internal decimal? ShippingAmt { get; set; }
        internal HostedPaymentData HostedPaymentData { get; set; }
        internal string InvoiceNumber { get; set; }
        internal bool Level2Request { get; set; }
        internal string OfflineAuthCode { get; set; }
        internal bool OneTimePayment { get; set; }
        internal string OrderId { get; set; }
        internal string ProductId { get; set; }
        internal RecurringSequence? RecurringSequence { get; set; }
        internal RecurringType? RecurringType { get; set; }
        internal bool RequestMultiUseToken { get; set; }
        internal GiftCard ReplacementCard { get; set; }
        internal string ScheduleId { get; set; }
        internal Address ShippingAddress { get; set; }
        internal string Timestamp { get; set; }

        /// <summary>
        /// Sets an address value; where applicable.
        /// </summary>
        /// <remarks>
        /// Currently supports billing and shipping addresses.
        /// </remarks>
        /// <param name="value">The desired address information</param>
        /// <param name="type">The desired address type</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithAddress(Address value, AddressType type = AddressType.Billing) {
            value.Type = type; // set the address type
            if (type == AddressType.Billing)
                BillingAddress = value;
            else ShippingAddress = value;
            return this;
        }

        internal AuthorizationBuilder WithAlias(AliasAction action, string value) {
            Alias = value;
            AliasAction = action;
            return this;
        }

        /// <summary>
        /// Allows duplicate transactions by skipping the
        /// gateway's duplicate checking.
        /// </summary>
        /// <param name="value">The duplicate skip flag</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithAllowDuplicates(bool value) {
            AllowDuplicates = value;
            return this;
        }

        /// <summary>
        /// Allows partial authorizations to occur.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="value">The allow partial flag</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithAllowPartialAuth(bool value) {
            AllowPartialAuth = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's amount
        /// </summary>
        /// <param name="value">The amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithAmount(decimal? value) {
            Amount = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's authorization amount; where applicable.
        /// </summary>
        /// <remarks>
        /// This is a specialized field. In most cases,
        /// `Authorization.WithAmount` should be used.
        /// </remarks>
        /// <param name="value">The authorization amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithAuthAmount(decimal? value) {
            AuthAmount = value;
            return this;
        }

        internal AuthorizationBuilder WithBalanceInquiryType(InquiryType? value) {
            BalanceInquiryType = value;
            return this;
        }

        /// <summary>
        /// Sets the cash back amount.
        /// </summary>
        /// <remarks>
        /// This is a specialized field for debit or EBT transactions.
        /// </remarks>
        /// <param name="value">The desired cash back amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCashBack(decimal? value) {
            CashBackAmount = value;
            TransactionModifier = TransactionModifier.CashBack;
            return this;
        }

        /// <summary>
        /// Sets the client transaction ID.
        /// </summary>
        /// <remarks>
        /// This is an application derived value that can be used to identify a
        /// transaction in case a gateway transaction ID is not returned, e.g.
        /// in cases of timeouts.
        ///
        /// The supplied value should be unique to the configured merchant or
        /// terminal account.
        /// </remarks>
        /// <param name="value">The client transaction ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithClientTransactionId(string value) {
            if (TransactionType == TransactionType.Reversal || TransactionType == TransactionType.Refund) {
                if (PaymentMethod is TransactionReference) {
                    ((TransactionReference)PaymentMethod).ClientTransactionId = value;
                }
                else {
                    PaymentMethod = new TransactionReference {
                        ClientTransactionId = value
                    };
                }
            }
            else ClientTransactionId = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's currency; where applicable.
        /// </summary>
        /// <remarks>
        /// The formatting for the supplied value will currently depend on
        /// the configured gateway's requirements.
        /// </remarks>
        /// <param name="value">The currency</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCurrency(string value) {
            Currency = value;
            return this;
        }

        /// <summary>
        /// Sets the customer ID; where applicable.
        /// </summary>
        /// <remarks>
        /// This is an application/merchant generated value.
        /// </remarks>
        /// <param name="value">The customer ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCustomerId(string value) {
            CustomerId = value;
            return this;
        }

        /// <summary>
        /// Sets the customer's IP address; where applicable.
        /// </summary>
        /// <remarks>
        /// This value should be obtained during the payment process.
        /// </remarks>
        /// <param name="value">The customer's IP address</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCustomerIpAddress(string value) {
            CustomerIpAddress = value;
            return this;
        }

        /// <summary>
        /// Sets the CVN value for recurring payments; where applicable.
        /// </summary>
        /// <param name="value">Cvn value to use in the request</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCvn(string value) {
            Cvn = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's description.
        /// </summary>
        /// <remarks>
        /// This value is not guaranteed to be sent in the authorization
        /// or settlement process.
        /// </remarks>
        /// <param name="value">The description</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithDescription(string value) {
            Description = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's dynamic descriptor.
        /// </summary>
        /// <remarks>
        /// This value is sent during the authorization process and is displayed
        /// in the consumer's account.
        /// </remarks>
        /// <param name="value">The dynamic descriptor</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithDynamicDescriptor(string value) {
            DynamicDescriptor = value;
            return this;
        }

        /// <summary>
        /// Sets eCommerce specific data; where applicable.
        /// </summary>
        /// <remarks>
        /// This can include:
        ///
        ///   - Consumer authentication (3DSecure) data
        ///   - Direct market data
        /// </remarks>
        /// <param name="value">The eCommerce data</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithEcommerceInfo(EcommerceInfo value) {
            EcommerceInfo = value;
            return this;
        }

        /// <summary>
        /// Sets the gratuity amount; where applicable.
        /// </summary>
        /// <remarks>
        /// This value is information only and does not affect
        /// the authorization amount.
        /// </remarks>
        /// <param name="value">The gratuity amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithGratuity(decimal? value) {
            Gratuity = value;
            return this;
        }

        /// <summary>
        /// Sets the Convenience amount; where applicable.
        /// </summary>       
        /// <param name="value">The Convenience amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithConvenienceAmt(decimal? value)
        {
            ConvenienceAmt = value;
            return this;
        }

        /// <summary>
        /// Sets the Shipping amount; where applicable.
        /// </summary>        
        /// <param name="value">The Shipping amount</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithShippingAmt(decimal? value)
        {
            ShippingAmt = value;
            return this;
        }

        /// <summary>
        /// Additional hosted payment specific information for Realex HPP implementation.
        /// </summary>
        /// <param name="value">The hosted payment data</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithHostedPaymentData(HostedPaymentData value) {
            var client = ServicesContainer.Instance.GetClient();
            if (client.SupportsHostedPayments) {
                HostedPaymentData = value;
                return this;
            }
            throw new UnsupportedTransactionException("You current gateway does not support hosted payments.");
        }

        /// <summary>
        /// Sets the invoice number; where applicable.
        /// </summary>
        /// <param name="value">The invoice number</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithInvoiceNumber(string value) {
            InvoiceNumber = value;
            return this;
        }

        /// <summary>
        /// Sets the commercial request flag; where applicable.
        /// </summary>
        /// <remarks>
        /// This flag indicates commercial purchase cards are accepted/expected.
        /// The application should inspect the transaction response and pass the
        /// appropriate Level II data when necessary.
        /// </remarks>
        /// <param name="value">The commercial request flag</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithCommercialRequest(bool value) {
            Level2Request = value;
            return this;
        }

        /// <summary>
        /// Sets the offline authorization code; where applicable.
        /// </summary>
        /// <remarks>
        /// The merchant is required to supply this value as obtained when
        /// calling the issuing bank for the authorization.
        /// </remarks>
        /// <param name="value">The offline authorization code</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithOfflineAuthCode(string value) {
            OfflineAuthCode = value;
            TransactionModifier = TransactionModifier.Offline;
            return this;
        }

        /// <summary>
        /// Sets the one-time payment flag; where applicable.
        /// </summary>
        /// <remarks>
        /// This is only useful when using recurring payment profiles for
        /// one-time payments that are not a part of a recurring schedule.
        /// </remarks>
        /// <param name="value">The one-time flag</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithOneTimePayment(bool value) {
            OneTimePayment = value;
            TransactionModifier = TransactionModifier.Recurring;
            return this;
        }

        /// <summary>
        /// Sets the transaction's order ID; where applicable.
        /// </summary>
        /// <param name="value">The order ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithOrderId(string value) {
            OrderId = value;
            return this;
        }

        /// <summary>
        /// Sets the transaction's payment method.
        /// </summary>
        /// <param name="value">The payment method</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithPaymentMethod(IPaymentMethod value) {
            PaymentMethod = value;
            if (value is EBTCardData && ((EBTCardData)value).SerialNumber != null)
                TransactionModifier = TransactionModifier.Voucher;
            return this;
        }

        /// <summary>
        /// Sets the transaction's product ID; where applicable.
        /// </summary>
        /// <param name="value">The product ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithProductId(string value) {
            ProductId = value;
            return this;
        }

        /// <summary>
        /// Sets the Recurring Info for Realex based recurring payments;
        /// where applicable.
        /// </summary>
        /// <param name="type">
        /// The value can be 'fixed' or 'variable' depending on whether
        /// the amount will change for each transaction.
        /// </param>
        /// <param name="sequence">
        /// Indicates where in the recurring sequence the transaction
        /// occurs. Must be 'first' for the first transaction for this
        /// card, 'subsequent' for transactions after that, and 'last'
        /// for the final transaction of the set.
        /// </param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithRecurringInfo(RecurringType type, RecurringSequence sequence) {
            RecurringSequence = sequence;
            RecurringType = type;
            return this;
        }

        /// <summary>
        /// Requests multi-use tokenization / card storage.
        /// </summary>
        /// <remarks>
        /// This will depend on a successful transaction. If there was a failure
        /// or decline, the multi-use tokenization / card storage will not be
        /// successful.
        /// </remarks>
        /// <param name="value">The request flag</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithRequestMultiUseToken(bool value) {
            RequestMultiUseToken = value;
            return this;
        }

        internal AuthorizationBuilder WithReplacementCard(GiftCard value) {
            ReplacementCard = value;
            return this;
        }

        /// <summary>
        /// Sets the schedule ID associated with the transaction; where applicable.
        /// </summary>
        /// <remarks>
        /// This is specific to transactions against recurring profiles that are
        /// a part of a recurring schedule.
        /// </remarks>
        /// <param name="value">The schedule ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithScheduleId(string value) {
            ScheduleId = value;
            return this;
        }

        /// <summary>
        /// Sets the related gateway transaction ID; where applicable.
        /// </summary>
        /// <remarks>
        /// This value is used to associated a previous transaction with the
        /// current transaction.
        /// </remarks>
        /// <param name="value">The gateway transaction ID</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithTransactionId(string value) {
            if (PaymentMethod is TransactionReference) {
                ((TransactionReference)PaymentMethod).TransactionId = value;
            }
            else {
                PaymentMethod = new TransactionReference {
                    TransactionId = value
                };
            }
            return this;
        }

        internal AuthorizationBuilder WithModifier(TransactionModifier value) {
            TransactionModifier = value;
            return this;
        }

        /// <summary>
        /// Sets the timestamp; where applicable.
        /// </summary>
        /// <param name="value">The transaction's timestamp</param>
        /// <returns>AuthorizationBuilder</returns>
        public AuthorizationBuilder WithTimestamp(string value) {
            Timestamp = value;
            return this;
        }


        internal AuthorizationBuilder(TransactionType type, IPaymentMethod payment = null) : base(type, payment) {}

        /// <summary>
        /// Executes the authorization builder against the gateway.
        /// </summary>
        /// <returns>Transaction</returns>
        public override Transaction Execute() {
            base.Execute();

            var client = ServicesContainer.Instance.GetClient();
            return client.ProcesAuthorization(this);
        }

        /// <summary>
        /// Serializes an authorization builder for hosted payment page requests.
        /// </summary>
        /// <remarks>
        /// Requires the gateway and account support hosted payment pages.
        /// </remarks>
        /// <returns>string</returns>
        public string Serialize() {
            TransactionModifier = TransactionModifier.HostedRequest;
            base.Execute();

            var client = ServicesContainer.Instance.GetClient();
            if (client.SupportsHostedPayments) {
                return client.SerializeRequest(this);
            }
            throw new UnsupportedTransactionException("You current gateway does not support hosted payments.");
        }

        protected override void SetupValidations() {
            Validations.For(TransactionType.Auth | TransactionType.Sale | TransactionType.Refund | TransactionType.AddValue)
                .With(TransactionModifier.None)
                .Check(() => Amount).IsNotNull()
                .Check(() => Currency).IsNotNull()
                .Check(() => PaymentMethod).IsNotNull();

            Validations.For(TransactionType.Auth | TransactionType.Sale)
                .With(TransactionModifier.HostedRequest)
                .Check(() => Amount).IsNotNull()
                .Check(() => Currency).IsNotNull();

            Validations.For(TransactionType.Verify)
                .With(TransactionModifier.HostedRequest)
                .Check(() => Currency).IsNotNull()
                .Check(() => Amount).IsNull();

            Validations.For(TransactionType.Auth | TransactionType.Sale)
                .With(TransactionModifier.Offline)
                .Check(() => Amount).IsNotNull()
                .Check(() => Currency).IsNotNull()
                .Check(() => OfflineAuthCode).IsNotNull();

            Validations.For(TransactionType.BenefitWithdrawal).With(TransactionModifier.CashBack)
                .Check(() => Amount).IsNotNull()
                .Check(() => Currency).IsNotNull()
                .Check(() => PaymentMethod).IsNotNull();

            Validations.For(TransactionType.Balance).Check(() => PaymentMethod).IsNotNull();

            Validations.For(TransactionType.Alias)
                .Check(() => AliasAction).IsNotNull()
                .Check(() => Alias).IsNotNull();

            Validations.For(TransactionType.Replace).Check(() => ReplacementCard).IsNotNull();

            Validations.For(PaymentMethodType.ACH).Check(() => BillingAddress).IsNotNull();
        }
    }
}