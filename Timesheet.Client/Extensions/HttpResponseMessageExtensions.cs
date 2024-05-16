using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Timesheet.Client.Models;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<(ResponseStatus status, string data, List<string> errors)> ToClientResponse(this HttpResponseMessage response)
    {
        ResponseStatus status = ResponseStatus.Success;
        string data = null;
        List<string> errors = null;

        var statusCode = (int)response.StatusCode;
        var result = await response.Content.ReadAsStringAsync();
        switch (statusCode)
        {
            case 200:
            case 201:
                status = ResponseStatus.Success;
                data = result;
                break;
            case 400:
                status = ResponseStatus.Error;
                errors = ProcessResponse400(result);
                break;
            case 500:
                status = ResponseStatus.Error;
                errors = ProcessResponse500(result);
                break;
            default:
                break;
        }

        return (status, data, errors);
    }

    private static List<string> ProcessResponse400(string result)
    {
        List<string> errors = new List<string>();

        try
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
            var errorsCount = (errorResponse.Errors as JObject).Count;

            if (errorResponse.Title.Contains("One or more validation errors occurred.") && errorsCount > 0)
            {
                foreach (var error in errorResponse.Errors)
                {
                    var asd = (error as JProperty).Value;

                    foreach (var item in asd)
                    {
                        errors.Add(item.Value<string>());
                    }
                }
            }
        }
        catch
        {
            errors.Add(result);
        }
        
        return errors;
    }

    private static List<string> ProcessResponse500(string result)
    {
        List<string> errors = new List<string>();
        errors.Add(result);

        return errors;
    }
}
