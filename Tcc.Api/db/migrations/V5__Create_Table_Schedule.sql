CREATE TABLE `schedule` (
	`id` BIGINT(19) NOT NULL DEFAULT '0',
	`schedule_date` DATETIME NOT NULL,
	`person_id` BIGINT(19) NOT NULL DEFAULT '0',
	`type_service_id` BIGINT(19) NOT NULL DEFAULT '0',
	INDEX `FK__person` (`person_id`) USING BTREE,
	INDEX `FK_type_service` (`type_service_id`) USING BTREE,
	CONSTRAINT `FK__person` FOREIGN KEY (`person_id`) REFERENCES `tcc_api`.`person` (`id`) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT `FK_type_service` FOREIGN KEY (`type_service_id`) REFERENCES `tcc_api`.`type_service` (`id`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COLLATE='utf8mb4_0900_ai_ci'
ENGINE=InnoDB
;