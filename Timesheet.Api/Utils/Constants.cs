namespace Timesheet.Api.Utils;

public static class Constants
{
    public const string FIELD_REQUIRED = "{PropertyName} cannot be null or empty";
    public const int EIGHHOURSSHIFT = 2;
    public const string BASETRYCATCHSQLSTATEMENT = @"DECLARE @Response int
                                                        BEGIN TRY
                                                            BEGIN TRANSACTION

                                                            {0}

                                                            COMMIT TRANSACTION
                                                            set @Response = 1
                                                        END TRY
                                                        BEGIN CATCH     
                                                            ROLLBACK TRANSACTION
                                                            set @Response = -1
                                                        END CATCH 
                                                        SELECT @Response AS Response
                                                       ";
}
