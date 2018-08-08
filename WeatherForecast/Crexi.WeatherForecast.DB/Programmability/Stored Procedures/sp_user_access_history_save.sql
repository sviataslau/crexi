CREATE PROCEDURE [dbo].[sp_user_access_history_save]
	@user_ip_address	nvarchar(20),
	@access_date		datetime,
	@user_agent			nvarchar(100) = null
AS
begin

	set nocount on;

	insert into [dbo].[UserAccessHistory]
	(
		[user_ip_address],
		[access_date],
		[user_agent]
	)
	values
	(
		@user_ip_address,
		@access_date,
		@user_agent
	)

end