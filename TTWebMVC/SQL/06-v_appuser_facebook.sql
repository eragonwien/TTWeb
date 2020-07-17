DROP VIEW v_appuser_facebook;
CREATE VIEW v_appuser_facebook AS
    SELECT 
        appuser.id AS appuser_id,
        facebookcredential.id as fb_credential_id,
        facebookcredential.fb_username,
        facebookcredential.fb_password
    FROM
        appuser
        INNER JOIN facebookcredential on appuser.id=facebookcredential.appuser_id
	WHERE
		appuser.active=1
        and appuser.disabled=0
        
