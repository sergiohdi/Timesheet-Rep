using Timesheet.Shared.Utils;

namespace Timesheet.Api.Utils;

public class UserRoleValidation
{
    private readonly int _extUserId;
    private readonly int _roleUserToken;
    private bool _validUserId;
    private bool _validRole;

    public UserRoleValidation(int extUserId, int roleUserToken)
    {
        _extUserId = extUserId;
        _roleUserToken = roleUserToken;
        _validUserId = false;
        _validRole = true;
    }

    public UserRoleValidation IsValidUserId()
    {
        if (_extUserId > 0)
        {
            _validUserId = true;
        }

        return this;
    }

    public UserRoleValidation IsNotAdminOrWtsRole()
    {
        if (_validUserId && (_roleUserToken != (int)UserRole.Admin && _roleUserToken != (int)UserRole.WTSAdmin))
        {
            _validRole = false;
        }

        return this;
    }

    public bool IsValidRole()
    {
        return _validRole;
    }
}
