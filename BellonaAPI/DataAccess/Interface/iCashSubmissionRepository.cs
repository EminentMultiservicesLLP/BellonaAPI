﻿using BellonaAPI.Models;
using System;
using System.Collections.Generic;

namespace BellonaAPI.DataAccess.Interface
{
    public interface ICashModuleRepository
    {
        IEnumerable<CashAuth> getCashAuth(int MenuId, Guid UserId, int OutletID = 0);
        IEnumerable<CashAuth> Authorize(int RequestId, string UserId);
        IEnumerable<CashDeposit> getCashDeposites(int MenuId, int OutletId, DateTime StartDate, DateTime EndDate, Guid UserId);
        bool savePendingCashDeposit(CashAuth cashAuth);
        bool deleteCashDeposits(string LoginId, int RequestID);
        IEnumerable<CashAuth> GetFilePath(int RequestId);
        IEnumerable<CashDepositStatus> GetCashDepositStatus(Guid UserId, int MenuId, string FromYear, int OutletId, int ClusterId, int CityId , int BrandId);
    }
}