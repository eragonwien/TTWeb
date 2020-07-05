DROP VIEW v_appuser_facebook;
CREATE VIEW v_appuser_facebook AS
    SELECT 
        appuser.id AS appuser_id,
        facebookcredentials.fb_username,
        facebookcredentials.fb_password
    FROM
        appuser
        LEFT JOIN facebookcredentials on appuser.id=facebookcredentials.appuser_id
	WHERE
		appuser.active=1
        and appuser.disabled=0
        
