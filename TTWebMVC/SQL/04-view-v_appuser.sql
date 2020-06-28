CREATE VIEW `v_appuser` AS
    SELECT 
        `appuser`.`id` AS `appuser_id`,
        `appuser`.`email` AS `email`,
        `appuser`.`password` AS `password`,
        `appuser`.`title` AS `title`,
        `appuser`.`firstname` AS `firstname`,
        `appuser`.`lastname` AS `lastname`,
        `appuser`.`disabled` AS `disabled`,
        `appuser`.`active` AS `active`,
        `appuser`.`facebook_user` AS `facebook_user`,
        `appuser`.`facebook_password` AS `facebook_password`,
        `role`.`id` AS `role_id`,
        `role`.`name` AS `role_name`,
        `role`.`description` AS `role_description`
    FROM
        ((`appuser`
        LEFT JOIN `appuserrole` ON ((`appuser`.`id` = `appuserrole`.`appuser_id`)))
        LEFT JOIN `role` ON ((`appuserrole`.`role_id` = `role`.`id`)));
