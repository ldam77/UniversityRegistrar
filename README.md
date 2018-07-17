# University Registrar
##### University Registrar database.

#### By Anousone Kaseumsouk, Lan Dam, Jean Jia, 07.17.2018

## Description

An app for a University registrar to keep track of students and courses.

## Setup

Install University Registrar by downloading the folder.

## Database
* Create a database in MySql and then import MySql file or create a new table with following commands.
* CREATE TABLE `students` ( `id` INT NOT NULL AUTO_INCREMENT , `name` VARCHAR(255) NOT NULL , `enrollment_date` VARCHAR(255) NOT NULL , PRIMARY KEY (`id`));
* CREATE TABLE `courses` ( `id` INT NOT NULL AUTO_INCREMENT , `course_name` VARCHAR(255) NOT NULL , `course_number` VARCHAR(255) NOT NULL , PRIMARY KEY (`id`));
* CREATE TABLE `students_courses` ( `id` INT NOT NULL AUTO_INCREMENT , `student_id` INT NOT NULL , `course_id` INT NOT NULL , PRIMARY KEY (`id`));
## Technologies Used

Application: CSharp, netcoreapp1.1, Razor, MAMP, MySQL

## Support and Contact

For any questions or support details, please email:
anousonekaseumsouk@icloud.com
ldam77@yahoo.com
jean84646@gmail.com

## Spec

* User can create a student entry and date of enrollment.
* User can create a course and keep track of all courses by name and course number.
* User can assign student to a course.

### Legal

Copyright (c) 2018 **Anousone Kaseumsouk, Lan Dam, and Jean Jia**

This software is licensed under the MIT license.
