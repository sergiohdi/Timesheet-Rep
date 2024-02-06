using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheet.Client.Extensions;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;
using Timesheet.Client.Utils;

namespace Timesheet.Client.Services.Implementations
{
    public class BaseDataService
    {
        private readonly HttpClient _client;
        private readonly IComponentComunicationService _notificationService;

        public BaseDataService(HttpClient client, IComponentComunicationService notificationService)
        {
            _client = client;
            _notificationService = notificationService;
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> GetList(string action, string filterValue = null)
        {
            action = !string.IsNullOrEmpty(filterValue) ? string.Concat(action, filterValue) : action;
            HttpResponseMessage response = await _client.GetAsync($"api/{action}");
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> GetById(string action, int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/{action}/{id}");
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> Create<T>(string action, T obj)
        {
            JsonContent jsonContent = JsonContent.Create(obj);
            HttpResponseMessage response = await _client.PostAsync($"api/{action}", jsonContent);
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> Update<T>(string action, T obj)
        {
            JsonContent jsonContent = JsonContent.Create(obj);
            HttpResponseMessage response = await _client.PutAsync($"api/{action}", jsonContent);
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> Delete(string action, int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/{action}/{id}");
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> Delete(string action)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/{action}");
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        public async Task<(ResponseStatus status, string data, List<string> errors)> Delete<T>(string action, T obj)
        {
            JsonContent jsonContent = JsonContent.Create(obj);
            HttpResponseMessage response = await _client.PostAsync($"api/{action}", jsonContent);
            var (status, data, errors) = await response.ToClientResponse();
            ValidateErrorResponse(status, errors);

            return (status, data, errors);
        }

        private void ValidateErrorResponse(ResponseStatus responseStatus, List<string> errors)
        {
            if (responseStatus == ResponseStatus.Error)
            {
                string message = string.Join(Environment.NewLine, errors);
                Console.WriteLine(message);

                _notificationService.SendNotification(new Notification
                {
                    NotificationType = NotificationType.Error,
                    Message = Constants.responseError
                });
            }
        }
    }
}
