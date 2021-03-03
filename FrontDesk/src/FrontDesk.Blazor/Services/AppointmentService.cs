﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models.Appointment;
using Microsoft.Extensions.Logging;

namespace FrontDesk.Blazor.Services
{
  public class AppointmentService
  {
    private readonly HttpService _httpService;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(HttpService httpService, ILogger<AppointmentService> logger)
    {
      _httpService = httpService;
      _logger = logger;
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentRequest appointment)
    {
      return (await _httpService.HttpPostAsync<CreateAppointmentResponse>(CreateAppointmentRequest.Route, appointment)).Appointment;
    }

    public async Task<AppointmentDto> EditAsync(UpdateAppointmentRequest appointment)
    {
      return (await _httpService.HttpPutAsync<UpdateAppointmentResponse>(UpdateAppointmentRequest.Route, appointment)).Appointment;
    }

    public Task DeleteAsync(Guid scheduleId, Guid appointmentId)
    {
      string route = GetByIdAppointmentRequest.Route.Replace("{scheduleId}", scheduleId.ToString());
      route = route.Replace("{appointmentId}", appointmentId.ToString());

      return _httpService.HttpDeleteAsync<DeleteAppointmentResponse>(route);
    }

    public async Task<AppointmentDto> GetByIdAsync(Guid scheduleId, Guid appointmentId)
    {
      string route = GetByIdAppointmentRequest.Route.Replace("{scheduleId}", scheduleId.ToString());
      route = route.Replace("{appointmentId}", appointmentId.ToString());
      return (await _httpService.HttpGetAsync<GetByIdAppointmentResponse>(route)).Appointment;
    }

    public async Task<List<AppointmentDto>> ListPagedAsync(int pageSize)
    {
      _logger.LogInformation("Fetching appointments from API.");

      return (await _httpService.HttpGetAsync<ListAppointmentResponse>(ListAppointmentRequest.Route)).Appointments;
    }

    public async Task<List<AppointmentDto>> ListAsync()
    {
      _logger.LogInformation("Fetching appointments from API.");

      return (await _httpService.HttpGetAsync<ListAppointmentResponse>(ListAppointmentRequest.Route)).Appointments;
    }
  }
}
