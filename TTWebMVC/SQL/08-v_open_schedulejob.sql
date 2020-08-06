CREATE OR REPLACE VIEW v_open_schedulejob AS
SELECT 
	def.id as id,
    def.appuser_id as appuser_id,
    def.friend_id as friend_id, 
    def.facebookcredential_id as facebookcredential_id, 
    def.name as name,
    def.type as type,
    def.interval_type as interval_type,
    def.time_from as time_from,
    def.time_to as time_to,
    def.timezone_id as timezone_id,
    def.active as active
FROM 
	schedulejobdef def 
	LEFT JOIN schedulejobdetail detail ON def.id=detail.schedulejobdef_id
	WHERE 
		def.active=1
		AND (detail.execution_time IS NULL OR DATE(detail.execution_time) < UTC_DATE());