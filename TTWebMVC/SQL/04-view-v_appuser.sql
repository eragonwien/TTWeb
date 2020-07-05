DROP VIEW v_appuser;
CREATE VIEW v_appuser AS
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
        role.description AS role_description
    FROM
        appuser
        LEFT JOIN role on appuser.role_id=role.id;
