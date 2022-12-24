using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using RentalCar.Service.Models;

// class VnPayLibrary: tạo URL để redirect tới trang thanh toán của VnPay và xác thực giao dịch
public class VnPayLibrary
{
    private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
    private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

    PaymentResponseDto paymentResponseDto;

    public PaymentResponseModel GetFullResponseData(PaymentResponseDto paymentResponseDto, string hashSecret)
    {
        var vnPay = new VnPayLibrary();

        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_Amount.ToString()))
        {
            vnPay.AddResponseData("vnp_Amount", paymentResponseDto.vnp_Amount.ToString());
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_BankCode))
        {
            vnPay.AddResponseData("vnp_BankCode", paymentResponseDto.vnp_BankCode);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_BankTranNo))
        {
            vnPay.AddResponseData("vnp_BankTranNo", paymentResponseDto.vnp_BankTranNo);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_CardType))
        {
            vnPay.AddResponseData("vnp_CardType", paymentResponseDto.vnp_CardType);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_OrderInfo))
        {
            vnPay.AddResponseData("vnp_OrderInfo", paymentResponseDto.vnp_OrderInfo);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_PayDate))
        {
            vnPay.AddResponseData("vnp_PayDate", paymentResponseDto.vnp_PayDate);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_ResponseCode))
        {
            vnPay.AddResponseData("vnp_ResponseCode", paymentResponseDto.vnp_ResponseCode);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_TmnCode))
        {
            vnPay.AddResponseData("vnp_TmnCode", paymentResponseDto.vnp_TmnCode);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_TransactionNo))
        {
            vnPay.AddResponseData("vnp_TransactionNo", paymentResponseDto.vnp_TransactionNo);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_TransactionStatus))
        {
            vnPay.AddResponseData("vnp_TransactionStatus", paymentResponseDto.vnp_TransactionStatus);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_TxnRef))
        {
            vnPay.AddResponseData("vnp_TxnRef", paymentResponseDto.vnp_TxnRef);
        }
        if (!string.IsNullOrEmpty(paymentResponseDto.vnp_SecureHash))
        {
            vnPay.AddResponseData("vnp_SecureHash", paymentResponseDto.vnp_SecureHash);
        }

        var checkSignature =
            vnPay.ValidateSignature(paymentResponseDto.vnp_SecureHash, hashSecret); //check Signature

        if (!checkSignature)
        {
            return new PaymentResponseModel()
            {
                Success = false
            };
        }
        return new PaymentResponseModel()
        {
            Success = true,
            PaymentMethod = "VnPay",
            OrderDescription = paymentResponseDto.vnp_OrderInfo,
            OrderId =paymentResponseDto.vnp_TxnRef, // orderId.ToString(),
            PaymentId = paymentResponseDto.vnp_TransactionNo, // vnPayTranId.ToString(),
            TransactionId = paymentResponseDto.vnp_TransactionNo, // vnPayTranId.ToString(),
            Token = paymentResponseDto.vnp_SecureHash,
            VnPayResponseCode = paymentResponseDto.vnp_ResponseCode
        };
    }
    public string GetIpAddress(HttpContext context)
    {
        var ipAddress = string.Empty;
        try
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;
        
            if (remoteIpAddress != null)
            {
                if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                }
        
                if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();
        
                return ipAddress;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return "127.0.0.1";
    }
    public void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }

    public void AddResponseData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }

    public string GetResponseData(string key)
    {
        return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
    }

    public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
    {
        var data = new StringBuilder();

        foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
        {
            data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        }

        var querystring = data.ToString();

        baseUrl += "?" + querystring;
        var signData = querystring;
        if (signData.Length > 0)
        {
            signData = signData.Remove(data.Length - 1, 1);
        }

        var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
        baseUrl += "vnp_SecureHash=" + vnpSecureHash;

        return baseUrl;
    }

    public bool ValidateSignature(string inputHash, string secretKey)
    {
        var rspRaw = GetResponseData();
        var myChecksum = HmacSha512(secretKey, rspRaw);
        var validate = myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        return validate;
    }

    private string HmacSha512(string key, string inputData)
    {
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }

    private string GetResponseData()
    {
        var data = new StringBuilder();
        if (_responseData.ContainsKey("vnp_SecureHashType"))
        {
            _responseData.Remove("vnp_SecureHashType");
        }

        if (_responseData.ContainsKey("vnp_SecureHash"))
        {
            _responseData.Remove("vnp_SecureHash");
        }

        foreach (var (key, value) in _responseData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
        {
            data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
        }

        //remove last '&'
        if (data.Length > 0)
        {
            data.Remove(data.Length - 1, 1);
        }

        return data.ToString();
    }
}

public class VnPayCompare : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
    }
}