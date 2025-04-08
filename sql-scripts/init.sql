-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: managementgraduationproject
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `classification`
--

DROP TABLE IF EXISTS `classification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `classification` (
  `ClassifiID` varchar(50) NOT NULL DEFAULT (uuid()),
  `TypeCode` varchar(50) DEFAULT NULL,
  `Code` varchar(50) DEFAULT NULL,
  `Value` text,
  `Role` varchar(50) DEFAULT NULL,
  `FileName` text,
  `Url` text,
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ClassifiID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classification`
--

LOCK TABLES `classification` WRITE;
/*!40000 ALTER TABLE `classification` DISABLE KEYS */;
INSERT INTO `classification` VALUES ('3be0b807-12b1-11f0-981e-00d861e0f8c2','ROLE_SYSTEM','STUDENT','Sinh viên','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be15f92-12b1-11f0-981e-00d861e0f8c2','ROLE_SYSTEM','TEACHER','Giảng viên','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be1da9e-12b1-11f0-981e-00d861e0f8c2','ROLE_SYSTEM','ADMIN','Quản trị viên','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be25014-12b1-11f0-981e-00d861e0f8c2','STATUS_SYSTEM','AUTH','Hoạt dộng','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be2bac4-12b1-11f0-981e-00d861e0f8c2','STATUS_SYSTEM','BLOCK','Đã khóa','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be326ca-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','PAUSE','Bảo lưu','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be38e97-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','DOING','Đang làm đồ án','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be3f958-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','ACCEPT','Được bảo vệ','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be46e9c-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','INTERN','Chỉ thực tập','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be4e7e2-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','REJECT','Không được bảo vệ','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be555f8-12b1-11f0-981e-00d861e0f8c2','STATUS_PROJECT','START','Mới tạo','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be5bba6-12b1-11f0-981e-00d861e0f8c2','TEMPLATE_FILE','REVIEW_MENTOR','Biểu mẫu đánh giá của giảng viên hướng dẫn','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be62654-12b1-11f0-981e-00d861e0f8c2','TEMPLATE_FILE','REVIEW_COMMENTATOR','Biểu mẫu đánh giá của giảng viên phản biện','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be6959c-12b1-11f0-981e-00d861e0f8c2','TEMPLATE_FILE','OUTLINE','Biểu mẫu đề cương đồ án','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be70868-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_NORMAL','Lịch thông báo','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be76b63-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_FOR_OUTLINE','Lịch chốt đề cương','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be7c9a7-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_FOR_MENTOR','Chốt điểm GVHD','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be82aa4-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_FOR_COMMENTATOR','Chốt điểm GVPB','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be89755-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_FINAL_FILE','Nộp báo cáo cuối cùng','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL),('3be90e39-12b1-11f0-981e-00d861e0f8c2','TYPE_SCHEDULE','SCHEDULE_FINAL_SCORE','Chốt điểm tổng kết','ADMIN',NULL,NULL,'2025-04-06 06:34:47',NULL);
/*!40000 ALTER TABLE `classification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `CommentId` varchar(50) NOT NULL DEFAULT (uuid()),
  `ContentComment` text,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UserName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`CommentId`),
  KEY `FK_Comment_ProjectOutline` (`UserName`),
  KEY `FK_Comment_Teacher` (`CreatedBy`),
  CONSTRAINT `FK_Comment_ProjectOutline` FOREIGN KEY (`UserName`) REFERENCES `projectoutline` (`UserName`),
  CONSTRAINT `FK_Comment_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `council`
--

DROP TABLE IF EXISTS `council`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `council` (
  `CouncilId` varchar(50) NOT NULL DEFAULT (uuid()),
  `CouncilName` varchar(100) DEFAULT NULL,
  `CouncilZoom` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `IsDelete` int DEFAULT '0',
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`CouncilId`),
  KEY `FK_Council_Semester` (`SemesterId`),
  CONSTRAINT `FK_Council_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `council`
--

LOCK TABLES `council` WRITE;
/*!40000 ALTER TABLE `council` DISABLE KEYS */;
/*!40000 ALTER TABLE `council` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detailscheduleweek`
--

DROP TABLE IF EXISTS `detailscheduleweek`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detailscheduleweek` (
  `ScheduleWeekId` varchar(50) NOT NULL,
  `UserNameProject` varchar(50) NOT NULL,
  `Comment` text,
  `NameFile` text,
  `SizeFile` text,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ScheduleWeekId`,`UserNameProject`),
  KEY `FK_DetailScheduleWeek_Project` (`UserNameProject`),
  CONSTRAINT `FK_DetailScheduleWeek_Project` FOREIGN KEY (`UserNameProject`) REFERENCES `project` (`UserName`),
  CONSTRAINT `FK_DetailScheduleWeek_ScheduleWeek` FOREIGN KEY (`ScheduleWeekId`) REFERENCES `scheduleweek` (`ScheduleWeekId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detailscheduleweek`
--

LOCK TABLES `detailscheduleweek` WRITE;
/*!40000 ALTER TABLE `detailscheduleweek` DISABLE KEYS */;
/*!40000 ALTER TABLE `detailscheduleweek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `education`
--

DROP TABLE IF EXISTS `education`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `education` (
  `EducationId` varchar(50) NOT NULL,
  `EducationName` text,
  `MaxStudentMentor` int DEFAULT NULL,
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`EducationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `education`
--

LOCK TABLES `education` WRITE;
/*!40000 ALTER TABLE `education` DISABLE KEYS */;
INSERT INTO `education` VALUES ('KS','Kỹ sư',20,'2025-04-06 06:34:47','hoangvanthong'),('ThS','Thạc sĩ',25,'2025-04-06 06:34:47','hoangvanthong'),('TS','Tiến sĩ',30,'2025-04-06 06:34:47','hoangvanthong');
/*!40000 ALTER TABLE `education` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groupreviewoutline`
--

DROP TABLE IF EXISTS `groupreviewoutline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `groupreviewoutline` (
  `GroupReviewOutlineId` varchar(50) NOT NULL DEFAULT (uuid()),
  `NameGroupReviewOutline` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `SemesterId` varchar(50) DEFAULT NULL,
  `IsDelete` int DEFAULT '0',
  PRIMARY KEY (`GroupReviewOutlineId`),
  KEY `FK_GroupReviewOutline_Semester` (`SemesterId`),
  CONSTRAINT `FK_GroupReviewOutline_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groupreviewoutline`
--

LOCK TABLES `groupreviewoutline` WRITE;
/*!40000 ALTER TABLE `groupreviewoutline` DISABLE KEYS */;
/*!40000 ALTER TABLE `groupreviewoutline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `major`
--

DROP TABLE IF EXISTS `major`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `major` (
  `MajorId` varchar(10) NOT NULL,
  `MajorName` text,
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`MajorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `major`
--

LOCK TABLES `major` WRITE;
/*!40000 ALTER TABLE `major` DISABLE KEYS */;
INSERT INTO `major` VALUES ('AI','Trí tuệ nhân tạo','2024-04-25 01:32:57','hoangvanthong'),('ATT','An toàn thông tin','2024-04-25 02:08:11','hoangvanthong'),('CNPM','Công nghệ phần mềm','2024-04-25 01:32:57','hoangvanthong'),('GAME','Lập trình game','2024-04-25 01:32:57','hoangvanthong'),('KHMT','Khoa học máy tính','2024-04-25 01:32:57','hoangvanthong'),('MANG','Mạng và HTTT','2024-04-25 01:32:57','hoangvanthong'),('TESTER','Kiểm thử phần mềm','2024-04-25 02:08:11','hoangvanthong');
/*!40000 ALTER TABLE `major` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `project` (
  `UserName` varchar(50) NOT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `CouncilId` varchar(50) DEFAULT NULL,
  `UserNameCommentator` varchar(50) DEFAULT NULL,
  `UserNameMentor` varchar(50) DEFAULT NULL,
  `ScoreMentor` float DEFAULT NULL,
  `CommentMentor` text,
  `CommentCommentator` text,
  `ScoreCommentator` float DEFAULT NULL,
  `ScoreUV1` float DEFAULT NULL,
  `CommentUV1` text,
  `ScoreUV2` float DEFAULT NULL,
  `CommentUV2` text,
  `ScoreUV3` float DEFAULT NULL,
  `CommentUV3` text,
  `ScoreTK` float DEFAULT NULL,
  `CommentTK` text,
  `ScoreCT` float DEFAULT NULL,
  `CommentCT` text,
  `ScoreFinal` float DEFAULT NULL,
  `StatusProject` varchar(50) DEFAULT 'START',
  `HashKeyMentor` text,
  `HashKeyCommentator` text,
  `NameFileFinal` text,
  `SizeFileFinal` text,
  `TypeFileFinal` text,
  PRIMARY KEY (`UserName`),
  KEY `FK_Project_Council` (`CouncilId`),
  KEY `FK_Project_Semester` (`SemesterId`),
  KEY `FK_Project_Teacher_Commentator` (`UserNameCommentator`),
  KEY `FK_Project_Teacher_Mentor` (`UserNameMentor`),
  CONSTRAINT `FK_Project_Council` FOREIGN KEY (`CouncilId`) REFERENCES `council` (`CouncilId`),
  CONSTRAINT `FK_Project_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  CONSTRAINT `FK_Project_Student` FOREIGN KEY (`UserName`) REFERENCES `student` (`UserName`),
  CONSTRAINT `FK_Project_Teacher_Commentator` FOREIGN KEY (`UserNameCommentator`) REFERENCES `teacher` (`UserName`),
  CONSTRAINT `FK_Project_Teacher_Mentor` FOREIGN KEY (`UserNameMentor`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project`
--

LOCK TABLES `project` WRITE;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
/*!40000 ALTER TABLE `project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `projectoutline`
--

DROP TABLE IF EXISTS `projectoutline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `projectoutline` (
  `UserName` varchar(50) NOT NULL,
  `NameProject` text,
  `PlantOutline` text,
  `TechProject` text,
  `ExpectResult` text,
  `ContentProject` text,
  `GroupReviewOutlineId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserName`),
  KEY `FK_ProjectOutline_GroupReviewOutline` (`GroupReviewOutlineId`),
  CONSTRAINT `FK_ProjectOutline_GroupReviewOutline` FOREIGN KEY (`GroupReviewOutlineId`) REFERENCES `groupreviewoutline` (`GroupReviewOutlineId`),
  CONSTRAINT `FK_ProjectOutline_Project` FOREIGN KEY (`UserName`) REFERENCES `project` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projectoutline`
--

LOCK TABLES `projectoutline` WRITE;
/*!40000 ALTER TABLE `projectoutline` DISABLE KEYS */;
/*!40000 ALTER TABLE `projectoutline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedulesemester`
--

DROP TABLE IF EXISTS `schedulesemester`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedulesemester` (
  `ScheduleSemesterId` varchar(50) NOT NULL DEFAULT (uuid()),
  `FromDate` datetime DEFAULT NULL,
  `ToDate` datetime DEFAULT NULL,
  `TypeSchedule` varchar(50) DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Title` text,
  `Implementer` varchar(200) DEFAULT NULL,
  `Content` text,
  `Note` text,
  `StatusSend` char(1) DEFAULT 'W',
  PRIMARY KEY (`ScheduleSemesterId`),
  KEY `FK_ScheduleSemester_Semester` (`SemesterId`),
  KEY `FK_ScheduleSemester_Teacher` (`CreatedBy`),
  CONSTRAINT `FK_ScheduleSemester_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  CONSTRAINT `FK_ScheduleSemester_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedulesemester`
--

LOCK TABLES `schedulesemester` WRITE;
/*!40000 ALTER TABLE `schedulesemester` DISABLE KEYS */;
/*!40000 ALTER TABLE `schedulesemester` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `scheduleweek`
--

DROP TABLE IF EXISTS `scheduleweek`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scheduleweek` (
  `ScheduleWeekId` varchar(50) NOT NULL DEFAULT (uuid()),
  `FromDate` datetime DEFAULT NULL,
  `ToDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `Title` text,
  `Content` text,
  PRIMARY KEY (`ScheduleWeekId`),
  KEY `FK_ScheduleWeek_Semester` (`SemesterId`),
  KEY `FK_ScheduleWeek_Teacher` (`CreatedBy`),
  CONSTRAINT `FK_ScheduleWeek_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  CONSTRAINT `FK_ScheduleWeek_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scheduleweek`
--

LOCK TABLES `scheduleweek` WRITE;
/*!40000 ALTER TABLE `scheduleweek` DISABLE KEYS */;
/*!40000 ALTER TABLE `scheduleweek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `semester`
--

DROP TABLE IF EXISTS `semester`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `semester` (
  `SemesterId` varchar(50) NOT NULL,
  `NameSemester` varchar(50) DEFAULT NULL,
  `FromDate` date DEFAULT NULL,
  `ToDate` date DEFAULT NULL,
  `ScheduleSemesterId` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `IsDelete` int DEFAULT '0',
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`SemesterId`),
  KEY `FK_Semester_Teacher` (`CreatedBy`),
  CONSTRAINT `FK_Semester_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `semester`
--

LOCK TABLES `semester` WRITE;
/*!40000 ALTER TABLE `semester` DISABLE KEYS */;
/*!40000 ALTER TABLE `semester` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `UserName` varchar(50) NOT NULL,
  `Password` blob,
  `FullName` text,
  `DOB` date DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Avatar` text,
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT 'AUTH',
  `StudentCode` varchar(30) DEFAULT NULL,
  `MajorId` varchar(10) DEFAULT NULL,
  `ClassName` varchar(50) DEFAULT NULL,
  `SchoolYearName` varchar(50) DEFAULT NULL,
  `IsDelete` int DEFAULT '0',
  `Token` text,
  `PasswordSalt` blob,
  `RefreshToken` text,
  `TokenCreated` datetime DEFAULT NULL,
  `TokenExpires` datetime DEFAULT NULL,
  `Gender` int DEFAULT NULL,
  `GPA` float DEFAULT NULL,
  `Address` text,
  `Role` varchar(50) DEFAULT 'STUDENT',
  `TypeFileAvatar` varchar(50) DEFAULT NULL,
  `UserNameMentorRegister` varchar(50) DEFAULT NULL,
  `IsFirstTime` int DEFAULT '1',
  PRIMARY KEY (`UserName`),
  KEY `FK_Student_Teacher_MentorRegister` (`UserNameMentorRegister`),
  KEY `FK_Student_Major` (`MajorId`),
  CONSTRAINT `FK_Student_Major` FOREIGN KEY (`MajorId`) REFERENCES `major` (`MajorId`),
  CONSTRAINT `FK_Student_Teacher_MentorRegister` FOREIGN KEY (`UserNameMentorRegister`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teacher`
--

DROP TABLE IF EXISTS `teacher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacher` (
  `UserName` varchar(50) NOT NULL,
  `FullName` text,
  `Password` blob,
  `DOB` date DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Avatar` text,
  `CreatedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT 'AUTH',
  `IsAdmin` int DEFAULT NULL,
  `IsDelete` int DEFAULT '0',
  `Token` text,
  `PasswordSalt` blob,
  `RefreshToken` text,
  `TokenCreated` datetime DEFAULT NULL,
  `TokenExpires` datetime DEFAULT NULL,
  `MajorId` varchar(10) DEFAULT NULL,
  `Gender` int DEFAULT NULL,
  `Role` varchar(50) DEFAULT 'TEACHER',
  `Address` text,
  `TypeFileAvatar` varchar(50) DEFAULT NULL,
  `EducationId` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`UserName`),
  KEY `FK_Teacher_Education` (`EducationId`),
  KEY `FK_Teacher_Major` (`MajorId`),
  CONSTRAINT `FK_Teacher_Education` FOREIGN KEY (`EducationId`) REFERENCES `education` (`EducationId`),
  CONSTRAINT `FK_Teacher_Major` FOREIGN KEY (`MajorId`) REFERENCES `major` (`MajorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher`
--

LOCK TABLES `teacher` WRITE;
/*!40000 ALTER TABLE `teacher` DISABLE KEYS */;
INSERT INTO `teacher` VALUES ('buingocdung','Bùi Ngọc Dũng',_binary 'm\�\� \�\�FN\�+U2��\�sW\�{_NN.\'~UL\"*Ef\�\��ڲ��\�\����[{o\0ۚ��}��t_a\�','1977-05-14','0913045130','bndung@gmail.com','','2024-04-10 01:47:40','hoangvanthong','BLOCK',0,0,NULL,_binary '9*\�U� �\�E\�\�\�\�\�l,�\�4\�\�b=\��\���v}b5$\�ϻ\�;\�_��8\�s�#>\0\�ˑaF�u$\�\�	.�X����\�)G\�3nl��!N�Py\�1$J���\�:F&����5ԏ��,\rY\"\�\�\�\�JX�',NULL,NULL,NULL,'MANG',0,'TEACHER','Hà Nội',NULL,'TS'),('caothiluyen',' Cao Thị Luyên',_binary '\��jv�\�a\���MrmE�[\�{\0o\�x\�Ym�G\�?p\�\�\�\�;��d6\��\�\�z\��\�\�\Z','1979-04-28','0912403345','caoluyengt@gmail.com','','2024-04-10 01:10:39','hoangvanthong','AUTH',0,0,NULL,_binary '�\�;_\�;�i�/x��\�x\�\�r�H\�\�V��O��\�G9͂\�\�x�r%?)\�\����\�A�q�\\\�\�\�4F�\r\�˕\�-���y\�.���l\"b\�L���ެI{��v\��C\�P�ywu��Ɵ\�7\�\�\�H�\�m�\�\�\Z�\�','maP0GJ0QTh3RKkypmCZnnjS2SryfNQxxaZ7c2dLoGbOfWvhZKilQHBrYQAuQSo/Nq/cMf0I2CaOziimz3ESUCw==','2024-04-15 17:12:24','2024-04-22 17:12:24','CNPM',0,'TEACHER','Hà Nội',NULL,'TS'),('daolethuy','Đào Thị Lệ Thủy',_binary '���\�&�\�\���_�\��\�\�eD\�\�\�\�%َ�wߴ\�\�\�=6.΁}pA\n)�\�B~\�?ST\�\�f','1976-01-29','0946921976','thuydtl@utc.edu.vn','','2024-04-10 01:09:19','hoangvanthong','AUTH',0,0,NULL,_binary '5��\n�s4\�[��Af��\'JԶ���_\�+�\�\�)�����Ѫ1\Z\�pt\�\�`\�`\�»� �\�\�N�u��:޻\�1����w�_�@�1ͨj���]\�:\�\�\r�	\�I7�@Z79�\�.Y}T=l ','I9oi14j+9lqASZ+o1J1IvpZjtAwNm6I+9GF4hfUjkkna1afCf6uB/jbj6pHhbFjPjtD+iWhYb1tiI6noCDwZMw==','2024-04-15 17:15:22','2024-04-22 17:15:22','CNPM',0,'TEACHER','Hà Nội',NULL,'ThS'),('dinhcongtung','Đinh Công Tùng',_binary '�n>Ù�X{�]A�\�F\�0��Y�Rq\�&)\�팏��w-KaɄ�\�T�\� \�D\��\�a��-\�Aq�','1997-09-26','0363641589','dinhcongtunggtvt@gmail.com','','2024-04-10 01:42:15','hoangvanthong','AUTH',0,0,NULL,_binary 'zK����\�\�\�ǭ�dXb&jcTt��1I�^yc�%�\�RF[\�^�\�K\�\�\��#\�j>�I�\�<S\\\�Ƙ\\7\�b4��t�\��J�\0\�+s.�٬\�\� \��K�غ�,|tZ{\�\�-A\�u$q̟\�T�<�\�{�','PtqNPNcEe89I8MMxdRNhKyLa6bZ9kvxvm3dXDgaCZfrXq/SRakEplcjrmRMftF/Df/RUUK07z8UZmblImBKPSg==','2024-05-02 10:47:17','2024-05-09 10:47:17','CNPM',0,'TEACHER','Hà Nội',NULL,'ThS'),('dovanduc','Đỗ Văn Đức',_binary '�\�\��\�*�6|vH�\�J�\'�DS/\�dP��\�?8�3�C(\�\�\Z4䖰Hi�\�\�t�n�;�8L','1979-12-23','0912324873','sy@gmail.com','','2024-04-10 01:40:25','hoangvanthong','AUTH',0,0,NULL,_binary '1\�\�\�\�[�J�w^\�\�q#\�\�\"-q\�\�7m�J�m(W;\�T\�\�(j{\�wB�\�7=\�.\�jñ#Q��Y\�8�d<��\�1K\\�T��3=I\�\0�yxs^vC\���H\�U?V0�\�R&@M�6\�\�50�%y	lI;l��>\���',NULL,NULL,NULL,'KHMT',0,'TEACHER','Hà Nội',NULL,'ThS'),('hoangvanthong','Hoàng Văn Thông',_binary '\�j�����Ȫ��ߢb��sU�\�BO�ĝ�[TEѯˮ�T^�H,	�\�\�\���dσ�#	\�t-','1979-07-18','0988113676','thonggtvt@gmail.com','fileuser	eacherhoangvanthongavatar_20240424215819.jpg','2024-04-10 00:46:17','hoangvanthong','AUTH',1,0,NULL,_binary '[���\�[zZ�X�VPK*�R�\�xE*Z\�p҈ �uÔe�g\�Bᷯ\�\�d.�`�v�,HfB��m�\�n�\�������\�Y;\\?�4X�z�߀\"w�?rc�M�\�\�EEq\r��6?cC\�n\Z��*�?��Y\�g','m09GtXW1HlKf957IAbJduZKgKjYw9BOGjXYdZxKL1fWGn0UK+SL0FSaMagX2CQtFBa5cz1CDeFWeW6mjCd7/ag==','2024-05-03 11:05:33','2024-05-10 11:05:33','KHMT',0,'ADMIN','Hà Nội','image/jpeg','TS'),('laimanhdung','Lại Mạnh Dũng',_binary '���yB\�ҖՖn*|ȗ�\nJ\�\�4]�i\�\�R	k�\�:c\�\�A���+���V�@\�\��\�m\�Wp�l\�','1981-08-06','0964978112','dunglm@gmail.com','','2024-04-10 01:46:02','hoangvanthong','AUTH',0,0,NULL,_binary 'ݪ�Wxz\Z\�X-o\�\Z�k��T�\�\\<\�w\�\�\Z?\�\�� �\�\�	\r�d\��+\�v3\�!4�\�ܢ6�I�vR>\�\�7Έؚ�j�C\�GG�\��bPM�!�ʨ��\�\�0tm\�I�;>�kB\�p&s;;�\�,p\�?�\�\�@蝑','dTw+erX3Q8SkJRL39x5SP9pXmlhxMthymNnFXx7QmYnIlBVZL4qa/6jcZkJnhuW/FvF3tQJfSaaBRSN4fhhXvw==','2024-05-02 10:43:28','2024-05-09 10:43:28','CNPM',0,'TEACHER','Hà Nội',NULL,'TS'),('luongthaile','Lương Thái Lê',_binary '\��|��3\�\�\���?�&_}^�p�����蜞!�m�曓�\�\�\Z{�,��\�s�\�\�CxN���M','1980-02-21','0973223450','luongthaile80@gmail.com','','2024-04-10 01:49:12','hoangvanthong','AUTH',0,0,NULL,_binary '�\�.�`v��\�Dnq\�\�\��*\�{\�1�4��5�&>(j\Z,���\�\�\'V�dp�f\��t�Y4\�\�\�gÁ�\�{X/M���{\���\�\�|�ZGr@[\�/�~�b\�0\��\�\�\�\�y\Z�\��Q_\'PrN1��I��\��)\��\�','+w0wWld+yTksWJjHmMAQ3Hygg4kaYPScTQsJah09mJQg1AA42Yt+RvOOCWTe1rP4xF6ZGxN37ZGlJLZ4WnapUQ==','2024-05-02 10:48:24','2024-05-09 10:48:24','KHMT',0,'TEACHER','Hà Nội',NULL,'TS'),('nguyenhieucuong','Nguyễn Hiếu Cường',_binary '��p\����O o\�B5��\Z�\�\r;\�ң5��>��|&g�\�\�\�\n\�\�5�L�[�L�\�\�;\�.4{y?�','2024-04-10','0967886712','cuonggt@gmail.com','fileuser	eacher\nguyenhieucuongavatar_20240426172512.jpg','2024-04-10 00:44:01','hoangvanthong','AUTH',1,0,NULL,_binary '�j��8\�?ؐ�\��`^\'K\� X!\�Ӥ��\"\�<5�5��{6\�>�yPmK\���\�R\��E���[���`�2\�\�[\�\�yKd�o�\"Kz����\�\�)\�4��z79�YdG�1}FG�D\�IT\�y_����\�X','XbyFEXRlAV6e+jVDAcXLPH1M6XxQ2ARf/MGRjkGS/aPL4MpqjXv2Vv+aEI5PYWDg2cpcKuAYpbzUU+Hbui+L7Q==','2024-05-03 10:33:48','2024-05-10 10:33:48','CNPM',0,'ADMIN','Hà Nội, Việt Nam','image/jpeg','TS'),('nguyenkimsao','Nguyễn Kim sao',_binary '\�law\�v\0�b�\�_�U��ve��FD�/�	6\�[\�\�\��\�Lw��\�/\���(\�\�\�\�#\�OC\�ܼ','1979-12-12','0905883993','saonkoliver@gmail.com','fileuser	eacher\nguyenkimsaoavatar_20240425174928.jpg','2024-04-10 01:44:31','hoangvanthong','AUTH',0,0,NULL,_binary 'U���\"`�%��j\0�\��{Җ\n9\�\��N	\�\�\�\�\�O��>a/�\�\"\�\�@�l�\�K���\�\�6[w��\�\�E��\��*X�ܤ�\�\�(H9\�Q\r��t\�Ҿ\r\0\�Xyb\�����\�$\�0�pM\\�\�tu$��S\�\��','5+QMqNW8KAKV0PGZzs3aoAuhvhm+9gizVlIMWRBVAN7NKxGp4zo1dziivcy5eDP70gxxd/P7WeQfa92Wud9nxw==','2024-04-25 18:05:12','2024-05-02 18:05:12','CNPM',0,'TEACHER','Hà Nội','image/jpeg','TS'),('nguyenquoctuan','Nguyễn Đức Dư',_binary 'o^\'�B�S�\0)DZ\�X�R�L:\�\�\�Cdi�\�\"H\�\�K\�d��\'\�]T�/ZQ7Sq,�m�\�\�\�','1979-09-14','0912363245','nducdu@gmail.com','','2024-04-10 00:48:38','hoangvanthong','AUTH',0,0,NULL,_binary '�[?{r��}\�s���p:7{�\0e\�*v\��_n��s\��鴲^\�\�s�\�ȩO\�\\4�\���\�.\�Sj�g���|�X�d�.8����\�$\��\�.Iwӛ��WA￠��\�GI\�~55J}u@\�\���\r\�Aɼ','vu409KBNQRXHKt2zpmK2xk1HejKWImZlz5C+LSsDbrq+Olx/bZf2LC4TZooMpa6o/zXaqlZeEtL3MVCqqmFbkA==','2024-05-02 10:49:38','2024-05-09 10:49:38','KHMT',0,'TEACHER','Hà Nội',NULL,'TS'),('nguyentranhieu','Nguyễn Trần Hiếu',_binary '���#%�(wB\�\�޹\0,Ș\�\�^\�*c<XN�\�g�	�\�f]\�\�c\�\�\�͒$\�ə�8\�6�Lj�w�W','1979-11-16','0912554558','nthieu@utc.edu.vn','','2024-04-10 01:58:18','hoangvanthong','AUTH',0,0,NULL,_binary '\r�}�g�4\�q:֑�h��\�Τ�`6�`\�]�\�{\�}5�<\�l�-\�\�d\��\0����=�U7ph\�Zz�y�ǂ\�`\�S7�ǳ\�~\"\���\����j���\�\�\�A\�\�k\�\�Gi�z�S\��)\0e;]��J����',NULL,NULL,NULL,'MANG',0,'TEACHER','Hà Nội',NULL,'ThS'),('nguyentrongphuc','Nguyễn Trọng Phúc',_binary '��c\���S\�\�L/T�l\�Bc��\�\�[�<,VO\��	\��p~A�*\�p\�O;��\����Sf\�\�\�','1976-10-02','0936298608','Phuc.NguyenTrong@nttdata.com','','2024-04-10 01:43:32','hoangvanthong','AUTH',0,0,NULL,_binary '�Ӄ��\�\�BQ@r\�b�CD\�\�3�c>�\\x��\�_��&=�\�F/\�J�T\�\��\Z�!mYl ��V\�GU�ɳ!\�j6\'�\�\�h�q7q4\�	�L\��,�\��\��\�ˬ>$��5`��7\�E7\�C�%\�\�3\Z/>',NULL,NULL,NULL,'CNPM',0,'TEACHER','Hà Nội',NULL,'TS'),('nguyenviethung','Nguyễn Việt Hưng',_binary '�[GF\�N\�b�6\��m\��\�(\�K\�tVf�\�M���\�L�ӵ\0T�{_ȣ\�%̷3ʠ\�P�N�zۣ','1992-05-25','0868004008','hungnv@utc.edu.vn','','2024-04-10 01:56:51','hoangvanthong','AUTH',0,0,NULL,_binary '�\��e��C\�7dh\Z�\�n;\�91\0��Ϭjc-`��OM�����K�S\�\�C3�A=\�F��ܔ\�4\�d\�])s\�«�X\rZ\�h#7[�5G\�H\�a��<v%�\�a\n��Q���\\c�Aq!\�\�w\�f\�h|�zn穛!fn�\�',NULL,NULL,NULL,'AI',0,'TEACHER','Hà Nội',NULL,'ThS'),('phamdinhphong','Phạm Đình Phong',_binary '�g\�\ZQTjhl�g���Vk\�Ad�\�\�;\�Q\�\�\�\�\�\�\�\�C��w4B��>����\�7qކG�/6j/','1976-09-04','0972481813','phongpd@utc.edu.vn','','2024-04-10 01:13:40','hoangvanthong','AUTH',0,0,NULL,_binary '�\�\�\'�Gr�A���+��<\nZ?k\�ɍN\�;(i)a[�Ǟ˱\�\'s|�\�5�B�\�Xy�\�\�R��Ƞ�C8��2GEB\�\���g�~�\�-(��jP�m�/\�\�\���\�\�?\"�\�\�V٬-�\�s\�','Ue+k4nKoMbsdznOyBTqBiT2urqwhcsXSeD8p6VZlwFOayvJPJS76Ocgj/U0//et+FahXJUGqrTPG8w8ynLOp6g==','2024-05-02 10:51:36','2024-05-09 10:51:36','AI',0,'TEACHER','Hà Nội',NULL,'TS'),('vuhuan','Vũ Huấn',_binary '� �}����!!蟂z\�\�9��u�1�\��d\���\0Fʫ\�\�\�c\�\�mV�mp\�#��$\�qB\�\�','1990-11-29','0988616090','huan.vu@utc.edu.vn','','2024-04-10 01:11:51','hoangvanthong','AUTH',0,0,NULL,_binary '\�,�\���\�m�n��x\�Ŧ��#\�\�\�Jv��\�#�#&\�o\�a��b�x�\�\�\�<X���#�\�\�WSC\�V��\�\�\������,\� \�P�\�g\�Г-�kT\�Qݍ\n�\\\�\�\�_��Ƚ�x\�\�>>�Ջ�\"G',NULL,NULL,NULL,'AI',0,'TEACHER','Hà Nội',NULL,'TS');
/*!40000 ALTER TABLE `teacher` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teaching`
--

DROP TABLE IF EXISTS `teaching`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teaching` (
  `UserNameTeacher` varchar(50) NOT NULL,
  `SemesterId` varchar(50) NOT NULL,
  `GroupReviewOutlineId` varchar(50) DEFAULT NULL,
  `CouncilId` varchar(50) DEFAULT NULL,
  `PositionInCouncil` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`SemesterId`,`UserNameTeacher`),
  KEY `FK_Teaching_Council` (`CouncilId`),
  KEY `FK_Teaching_GroupReviewOutline` (`GroupReviewOutlineId`),
  KEY `FK_Teaching_Teacher` (`UserNameTeacher`),
  CONSTRAINT `FK_Teaching_Council` FOREIGN KEY (`CouncilId`) REFERENCES `council` (`CouncilId`),
  CONSTRAINT `FK_Teaching_GroupReviewOutline` FOREIGN KEY (`GroupReviewOutlineId`) REFERENCES `groupreviewoutline` (`GroupReviewOutlineId`),
  CONSTRAINT `FK_Teaching_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  CONSTRAINT `FK_Teaching_Teacher` FOREIGN KEY (`UserNameTeacher`) REFERENCES `teacher` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teaching`
--

LOCK TABLES `teaching` WRITE;
/*!40000 ALTER TABLE `teaching` DISABLE KEYS */;
/*!40000 ALTER TABLE `teaching` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'managementgraduationproject'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-06 13:36:13
