using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    //The purpose of this class is to get the error stsck trace along with the response
    public class ApiException : ApiResponse
    {

        public ApiException(int statusCode, string message = null, string details = null) : base
        (statusCode, message)
        {
            Details = details; 

        }
    public string Details { get; set; }
        
    }
}