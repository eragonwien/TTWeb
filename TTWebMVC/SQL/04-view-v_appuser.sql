CREATE OR REPLACE VIEW v_appuser AS
    SELECT 
        appuser.id AS appuser_id,
        appuser.email AS email,
        appuser.title AS title,
        appuser.firstname AS firstname,
        appuser.lastname AS lastname,
        appuser.disabled AS disabled,
        appuser.active AS active,
        role.id AS role_id,
        role.name AS role_name,
        role.description AS role_description,
        facebookcredential.id as fb_credential_id,
        facebookcredential.fb_username,
        facebookcredential.fb_password
    FROM
        appuser
        LEFT JOIN facebookcredential on appuser.id=facebookcredential.appuser_id
        LEFT JOIN role on appuser.role_id=role.id;