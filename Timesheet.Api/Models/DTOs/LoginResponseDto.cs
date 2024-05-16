namespace Timesheet.Api.Models.DTOs;

public record LoginResponseDto(bool IsSuccess, string Token, bool ForcePasswordChange);

