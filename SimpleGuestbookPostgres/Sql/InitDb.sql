﻿DROP TABLE IF EXISTS posts;

CREATE TABLE posts (
	"id" int PRIMARY KEY GENERATED BY DEFAULT AS IDENTITY,
	"Posttext" TEXT NOT NULL,
	"Username" TEXT NOT NULL,
	"CreatedOn" DATE NOT NULL
)