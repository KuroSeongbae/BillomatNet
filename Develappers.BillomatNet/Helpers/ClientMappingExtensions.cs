﻿using System;
using System.Linq;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Types;
using Client = Develappers.BillomatNet.Types.Client;

namespace Develappers.BillomatNet.Helpers
{
    internal static class ClientMappingExtensions
    {
        internal static Types.PagedList<Client> ToDomain(this ClientListWrapper value)
        {
            return value?.Item.ToDomain();
        }

        internal static Types.PagedList<Client> ToDomain(this ClientList value)
        {
            if (value == null)
            {
                return null;
            }

            return new Types.PagedList<Client>
            {
                Page = value.Page,
                ItemsPerPage = value.PerPage,
                TotalItems = value.Total,
                List = value.List?.Select(ToDomain).ToList()
            };
        }

        internal static Client ToDomain(this ClientWrapper value)
        {
            return value?.Client.ToDomain();
        }

        private static Client ToDomain(this Api.Client value)
        {
            if (value == null)
            {
                return null;
            }

            NetGrossSettingsType netGrossSettingsType;

            switch (value.NetGross.ToLowerInvariant())
            {
                case "net":
                    netGrossSettingsType = NetGrossSettingsType.Net;
                    break;
                case "gross":
                    netGrossSettingsType = NetGrossSettingsType.Gross;
                    break;
                case "settings":
                    netGrossSettingsType = NetGrossSettingsType.Settings;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Client
            {
                Id = int.Parse(value.Id),
                Number = value.Number,
                CountryCode = value.CountryCode,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Note = value.Note,
                Tags = value.Tags.ToStringList(),
                InvoiceIds = value.InvoiceId.ToIntList(),
                CreatedAt = value.Created,
                IsArchived = value.Archived != "0",
                NumberPrefix = value.NumberPre,
                NumberLength = int.Parse(value.NumberLength),
                Address = value.Address,
                ClientNumber = value.ClientNumber,
                BankAccountNumber = value.BankAccountNumber,
                BankAccountOwner = value.BankAccountOwner,
                BankIban = value.BankIban,
                BankName = value.BankName,
                BankNumber = value.BankNumber,
                BankSwift = value.BankSwift,
                City = value.City,
                CurrencyCode = value.CurrencyCode,
                Fax = value.Fax,
                Mobile = value.Mobile,
                Name = value.Name,
                Phone = value.Phone,
                Salutation = value.Salutation,
                State = value.State,
                Street = value.Street,
                TaxNumber = value.TaxNumber,
                VatNumber = value.VatNumber,
                Web = value.Www,
                ZipCode = value.Zip,
                NetGross = netGrossSettingsType
            };
        }

    }
}