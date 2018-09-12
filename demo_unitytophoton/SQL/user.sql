create database demo;
use demo;
create table user(
    `id` int unsigned auto_increment,
    `name` varchar(30) not null unique,
    `password` varchar(30),
    `msg` varchar(100),
    primary key(`id`)
    );