CREATE DATABASE managementgraduationproject
-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th4 06, 2025 lúc 09:57 AM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `managementgraduationproject`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `classification`
--

CREATE TABLE `classification` (
  `ClassifiID` varchar(50) NOT NULL DEFAULT uuid(),
  `TypeCode` varchar(50) DEFAULT NULL,
  `Code` varchar(50) DEFAULT NULL,
  `Value` text DEFAULT NULL,
  `Role` varchar(50) DEFAULT NULL,
  `FileName` text DEFAULT NULL,
  `Url` text DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `CreatedBy` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `classification`
--

INSERT INTO `classification` (`ClassifiID`, `TypeCode`, `Code`, `Value`, `Role`, `FileName`, `Url`, `CreatedAt`, `CreatedBy`) VALUES
('b8aa12bf-12bc-11f0-b096-00d861e0f8c2', 'ROLE_SYSTEM', 'STUDENT', 'Sinh viên', 'ADMIN', NULL, NULL, '2025-04-06 07:57:00', NULL),
('b9006911-12bc-11f0-b096-00d861e0f8c2', 'ROLE_SYSTEM', 'TEACHER', 'Giảng viên', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b900e021-12bc-11f0-b096-00d861e0f8c2', 'ROLE_SYSTEM', 'ADMIN', 'Quản trị viên', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9014b1b-12bc-11f0-b096-00d861e0f8c2', 'STATUS_SYSTEM', 'AUTH', 'Hoạt dộng', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b901c7e4-12bc-11f0-b096-00d861e0f8c2', 'STATUS_SYSTEM', 'BLOCK', 'Đã khóa', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9022f75-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'PAUSE', 'Bảo lưu', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b902a1ff-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'DOING', 'Đang làm đồ án', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b90310d2-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'ACCEPT', 'Được bảo vệ', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9037fd5-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'INTERN', 'Chỉ thực tập', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b903e45b-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'REJECT', 'Không được bảo vệ', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9045909-12bc-11f0-b096-00d861e0f8c2', 'STATUS_PROJECT', 'START', 'Mới tạo', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b904ca04-12bc-11f0-b096-00d861e0f8c2', 'TEMPLATE_FILE', 'REVIEW_MENTOR', 'Biểu mẫu đánh giá của giảng viên hướng dẫn', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b90548e1-12bc-11f0-b096-00d861e0f8c2', 'TEMPLATE_FILE', 'REVIEW_COMMENTATOR', 'Biểu mẫu đánh giá của giảng viên phản biện', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b905be83-12bc-11f0-b096-00d861e0f8c2', 'TEMPLATE_FILE', 'OUTLINE', 'Biểu mẫu đề cương đồ án', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9062c34-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_NORMAL', 'Lịch thông báo', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b906acb8-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_FOR_OUTLINE', 'Lịch chốt đề cương', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9072e0a-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_FOR_MENTOR', 'Chốt điểm GVHD', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b907a15c-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_FOR_COMMENTATOR', 'Chốt điểm GVPB', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b90817d8-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_FINAL_FILE', 'Nộp báo cáo cuối cùng', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL),
('b9088fe7-12bc-11f0-b096-00d861e0f8c2', 'TYPE_SCHEDULE', 'SCHEDULE_FINAL_SCORE', 'Chốt điểm tổng kết', 'ADMIN', NULL, NULL, '2025-04-06 07:57:01', NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `comment`
--

CREATE TABLE `comment` (
  `CommentId` varchar(50) NOT NULL DEFAULT uuid(),
  `ContentComment` text DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT current_timestamp(),
  `UserName` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `council`
--

CREATE TABLE `council` (
  `CouncilId` varchar(50) NOT NULL DEFAULT uuid(),
  `CouncilName` varchar(100) DEFAULT NULL,
  `CouncilZoom` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `IsDelete` int(11) DEFAULT 0,
  `CreatedDate` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `detailscheduleweek`
--

CREATE TABLE `detailscheduleweek` (
  `ScheduleWeekId` varchar(50) NOT NULL,
  `UserNameProject` varchar(50) NOT NULL,
  `Comment` text DEFAULT NULL,
  `NameFile` text DEFAULT NULL,
  `SizeFile` text DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `education`
--

CREATE TABLE `education` (
  `EducationId` varchar(50) NOT NULL,
  `EducationName` text DEFAULT NULL,
  `MaxStudentMentor` int(11) DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `CreatedBy` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `education`
--

INSERT INTO `education` (`EducationId`, `EducationName`, `MaxStudentMentor`, `CreatedAt`, `CreatedBy`) VALUES
('KS', 'Kỹ sư', 20, '2025-04-06 07:57:01', 'hoangvanthong'),
('ThS', 'Thạc sĩ', 25, '2025-04-06 07:57:01', 'hoangvanthong'),
('TS', 'Tiến sĩ', 30, '2025-04-06 07:57:01', 'hoangvanthong');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `groupreviewoutline`
--

CREATE TABLE `groupreviewoutline` (
  `GroupReviewOutlineId` varchar(50) NOT NULL DEFAULT uuid(),
  `NameGroupReviewOutline` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT current_timestamp(),
  `SemesterId` varchar(50) DEFAULT NULL,
  `IsDelete` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `major`
--

CREATE TABLE `major` (
  `MajorId` varchar(10) NOT NULL,
  `MajorName` text DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `CreatedBy` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `major`
--

INSERT INTO `major` (`MajorId`, `MajorName`, `CreatedAt`, `CreatedBy`) VALUES
('AI', 'Trí tuệ nhân tạo', '2024-04-25 01:32:56', 'hoangvanthong'),
('ATT', 'An toàn thông tin', '2024-04-25 02:08:10', 'hoangvanthong'),
('CNPM', 'Công nghệ phần mềm', '2024-04-25 01:32:56', 'hoangvanthong'),
('GAME', 'Lập trình game', '2024-04-25 01:32:56', 'hoangvanthong'),
('KHMT', 'Khoa học máy tính', '2024-04-25 01:32:56', 'hoangvanthong'),
('MANG', 'Mạng và HTTT', '2024-04-25 01:32:56', 'hoangvanthong'),
('TESTER', 'Kiểm thử phần mềm', '2024-04-25 02:08:10', 'hoangvanthong');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `project`
--

CREATE TABLE `project` (
  `UserName` varchar(50) NOT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `CouncilId` varchar(50) DEFAULT NULL,
  `UserNameCommentator` varchar(50) DEFAULT NULL,
  `UserNameMentor` varchar(50) DEFAULT NULL,
  `ScoreMentor` float DEFAULT NULL,
  `CommentMentor` text DEFAULT NULL,
  `CommentCommentator` text DEFAULT NULL,
  `ScoreCommentator` float DEFAULT NULL,
  `ScoreUV1` float DEFAULT NULL,
  `CommentUV1` text DEFAULT NULL,
  `ScoreUV2` float DEFAULT NULL,
  `CommentUV2` text DEFAULT NULL,
  `ScoreUV3` float DEFAULT NULL,
  `CommentUV3` text DEFAULT NULL,
  `ScoreTK` float DEFAULT NULL,
  `CommentTK` text DEFAULT NULL,
  `ScoreCT` float DEFAULT NULL,
  `CommentCT` text DEFAULT NULL,
  `ScoreFinal` float DEFAULT NULL,
  `StatusProject` varchar(50) DEFAULT 'START',
  `HashKeyMentor` text DEFAULT NULL,
  `HashKeyCommentator` text DEFAULT NULL,
  `NameFileFinal` text DEFAULT NULL,
  `SizeFileFinal` text DEFAULT NULL,
  `TypeFileFinal` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `projectoutline`
--

CREATE TABLE `projectoutline` (
  `UserName` varchar(50) NOT NULL,
  `NameProject` text DEFAULT NULL,
  `PlantOutline` text DEFAULT NULL,
  `TechProject` text DEFAULT NULL,
  `ExpectResult` text DEFAULT NULL,
  `ContentProject` text DEFAULT NULL,
  `GroupReviewOutlineId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `schedulesemester`
--

CREATE TABLE `schedulesemester` (
  `ScheduleSemesterId` varchar(50) NOT NULL DEFAULT uuid(),
  `FromDate` datetime DEFAULT NULL,
  `ToDate` datetime DEFAULT NULL,
  `TypeSchedule` varchar(50) DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT current_timestamp(),
  `Title` text DEFAULT NULL,
  `Implementer` varchar(200) DEFAULT NULL,
  `Content` text DEFAULT NULL,
  `Note` text DEFAULT NULL,
  `StatusSend` char(1) DEFAULT 'W'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `scheduleweek`
--

CREATE TABLE `scheduleweek` (
  `ScheduleWeekId` varchar(50) NOT NULL DEFAULT uuid(),
  `FromDate` datetime DEFAULT NULL,
  `ToDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `SemesterId` varchar(50) DEFAULT NULL,
  `Title` text DEFAULT NULL,
  `Content` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `semester`
--

CREATE TABLE `semester` (
  `SemesterId` varchar(50) NOT NULL,
  `NameSemester` varchar(50) DEFAULT NULL,
  `FromDate` date DEFAULT NULL,
  `ToDate` date DEFAULT NULL,
  `ScheduleSemesterId` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `IsDelete` int(11) DEFAULT 0,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `student`
--

CREATE TABLE `student` (
  `UserName` varchar(50) NOT NULL,
  `Password` blob DEFAULT NULL,
  `FullName` text DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Avatar` text DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `CreatedBy` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT 'AUTH',
  `StudentCode` varchar(30) DEFAULT NULL,
  `MajorId` varchar(10) DEFAULT NULL,
  `ClassName` varchar(50) DEFAULT NULL,
  `SchoolYearName` varchar(50) DEFAULT NULL,
  `IsDelete` int(11) DEFAULT 0,
  `Token` text DEFAULT NULL,
  `PasswordSalt` blob DEFAULT NULL,
  `RefreshToken` text DEFAULT NULL,
  `TokenCreated` datetime DEFAULT NULL,
  `TokenExpires` datetime DEFAULT NULL,
  `Gender` int(11) DEFAULT NULL,
  `GPA` float DEFAULT NULL,
  `Address` text DEFAULT NULL,
  `Role` varchar(50) DEFAULT 'STUDENT',
  `TypeFileAvatar` varchar(50) DEFAULT NULL,
  `UserNameMentorRegister` varchar(50) DEFAULT NULL,
  `IsFirstTime` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `teacher`
--

CREATE TABLE `teacher` (
  `UserName` varchar(50) NOT NULL,
  `FullName` text DEFAULT NULL,
  `Password` blob DEFAULT NULL,
  `DOB` date DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Avatar` text DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `CreatedBy` varchar(50) DEFAULT NULL,
  `Status` varchar(50) DEFAULT 'AUTH',
  `IsAdmin` int(11) DEFAULT NULL,
  `IsDelete` int(11) DEFAULT 0,
  `Token` text DEFAULT NULL,
  `PasswordSalt` blob DEFAULT NULL,
  `RefreshToken` text DEFAULT NULL,
  `TokenCreated` datetime DEFAULT NULL,
  `TokenExpires` datetime DEFAULT NULL,
  `MajorId` varchar(10) DEFAULT NULL,
  `Gender` int(11) DEFAULT NULL,
  `Role` varchar(50) DEFAULT 'TEACHER',
  `Address` text DEFAULT NULL,
  `TypeFileAvatar` varchar(50) DEFAULT NULL,
  `EducationId` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `teacher`
--

INSERT INTO `teacher` (`UserName`, `FullName`, `Password`, `DOB`, `Phone`, `Email`, `Avatar`, `CreatedAt`, `CreatedBy`, `Status`, `IsAdmin`, `IsDelete`, `Token`, `PasswordSalt`, `RefreshToken`, `TokenCreated`, `TokenExpires`, `MajorId`, `Gender`, `Role`, `Address`, `TypeFileAvatar`, `EducationId`) VALUES
('buingocdung', 'Bùi Ngọc Dũng', 0x6de5ca01021c20cef0464edb2b55329ac1d8067357ed7b5f4e4e2e277e554c222a4566ee1ec2ff15dab288c0dfdbff96b05b7b6f00db9af9917d8ab5745f61e1, '1977-05-14', '0913045130', 'bndung@gmail.com', '', '2024-04-10 01:47:39', 'hoangvanthong', 'BLOCK', 0, 0, NULL, 0x18392a1ce75508802095d745de1bf4ea8ff192eb0e6c2cbeed34ccee623de51396f4abb518767d623524eccfbbd23bee0f5fbda238c2739e233e00c21dcb916146b27524f5de092e9a588904a1ad9fe0294701e7336e6cb5a11b214eaa5079de31244aa5f88cee173a46269c97908635d48f85bc2c0d5922d7f314dacf4a5881, NULL, NULL, NULL, 'MANG', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('caothiluyen', ' Cao Thị Luyên', 0x0201f50604c16a76bfc461de04bea94d726d45995befaf7b006fc478c7596da347f095113f70e4efafe3afd53b0602af9c076436d80e9adbe67ac7148ce5dd1a, '1979-04-28', '0912403345', 'caoluyengt@gmail.com', '', '2024-04-10 01:10:38', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0xc01cca3b5fe7b43b85698e2f7898bbcc78e8e6728b4801ddd01b56a1ae4fb6a2d34739cd82e7d078b272253f29f7f61dc01d8bf9e841bc71805cd7c9dc3446ac0dd6cb95e42d98959e790bd02e89acad6c2262f34c80b486deac497b90ad76e1ff4308e79450b47902777587bac69fe637e5d3f748b6e9bc196d8cd1f51aa4d3, 'maP0GJ0QTh3RKkypmCZnnjS2SryfNQxxaZ7c2dLoGbOfWvhZKilQHBrYQAuQSo/Nq/cMf0I2CaOziimz3ESUCw==', '2024-04-15 17:12:23', '2024-04-22 17:12:23', 'CNPM', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('daolethuy', 'Đào Thị Lệ Thủy', 0xb398a3c726aded1de213be975f93e31e90cfe86544dbf1a4f5df25d98e159d77dfb4cef201e0a73d362ece817d0e70410a29b7da42027edd3f145354e6bfeb66, '1976-01-29', '0946921976', 'thuydtl@utc.edu.vn', '', '2024-04-10 01:09:19', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x35b6bf0abd05731f34f45b9883184166b60495274ad4b68bfbb75fc52b13b6d012f429a707ad9eb9a4d1aa311af482ba0b0f7074d9f060ee60f1c2bb81209ac4ea4e0195759c0e843adebbe17f31908cfb861c7714179d5f901e40921c31cda86af984aa5df33acde90d1bf909dc493716b240185a3739b1dc2e597d543d6c20, 'I9oi14j+9lqASZ+o1J1IvpZjtAwNm6I+9GF4hfUjkkna1afCf6uB/jbj6pHhbFjPjtD+iWhYb1tiI6noCDwZMw==', '2024-04-15 17:15:21', '2024-04-22 17:15:21', 'CNPM', 0, 'TEACHER', 'Hà Nội', NULL, 'ThS'),
('dinhcongtung', 'Đinh Công Tùng', 0xbb6e3ec39980587b815d419cca46d930ad88599c5271d12629eced8c8fb4b00b772d4b61c9849b0cf1b19154fedb20f644d10483e99a61111f85c02dd9417182, '1997-09-26', '0363641589', 'dinhcongtunggtvt@gmail.com', '', '2024-04-10 01:42:14', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x7a4b908ff9bbd0edf3c7ad1ba1645862266a63547499893149805e137963b82510afeea152465bcb5eb7db4be6cb06d6f823e66a1b3ea31449fcf51b3c535ce7c6985c37ee6234aeb3741518bae8bfff144aab0e00cb2b732e941dd9acdcf620c4fe4bae15d8baaf2c7c745a7bead72d41c2752471cc9fe45406803ca1f17bbc, 'PtqNPNcEe89I8MMxdRNhKyLa6bZ9kvxvm3dXDgaCZfrXq/SRakEplcjrmRMftF/Df/RUUK07z8UZmblImBKPSg==', '2024-05-02 10:47:17', '2024-05-09 10:47:17', 'CNPM', 0, 'TEACHER', 'Hà Nội', NULL, 'ThS'),
('dovanduc', 'Đỗ Văn Đức', 0x95e583f3148dd22a050e8912367c764886c34a8c279d44532fcf6450aba0e4a63f38a03301bb4328dc18d31a34e496b0481d699f05d50cc874836efa3b9b384c, '1979-12-23', '0912324873', 'sy@gmail.com', '', '2024-04-10 01:40:24', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x31f0f0dcd65ba94aad775ec2cd711c23f6ee222d710ee6d4376d914abb6d28573be854d5e1286a107b19ed7742b8ed373dcf2eee6ac3b12351c1b659d738b9643cfefcdb314b5c915407a598333d49dc00917978735e7643c8fc10a21248cf55113f5630ffd6522640064db136cc13f33530982579096c493b6cc19b3ef6ba8a, NULL, NULL, NULL, 'KHMT', 0, 'TEACHER', 'Hà Nội', NULL, 'ThS'),
('hoangvanthong', 'Hoàng Văn Thông', 0xc76a830efcbd94f8c8aab38adfa26299ba73558bd4424f1b89c49d925b5445d1af1615cbaebf545e169f482c1309a4caf4ecc095647fcf83b42309e88e74192d, '1979-07-18', '0988113676', 'thonggtvt@gmail.com', 'fileuser	eacherhoangvanthongavatar_20240424215819.jpg', '2024-04-10 00:46:17', 'hoangvanthong', 'AUTH', 1, 0, NULL, 0x5baf990385f1825b7a5aff588b56130c504b112a99529ed47807452a5ace70d28820a075c39465a367e48a42e1b7afefc4642e9d609976802c7f08486642a8bf6d85e3ad6e84e2a1fb88a29abab507cc593b5c3f9e3458817a93df802277a53f7263804da3e1c64545710db9fb363f6343cd6e1aa1f92aab3fb983590e06dd67, 'm09GtXW1HlKf957IAbJduZKgKjYw9BOGjXYdZxKL1fWGn0UK+SL0FSaMagX2CQtFBa5cz1CDeFWeW6mjCd7/ag==', '2024-05-03 11:05:33', '2024-05-10 11:05:33', 'KHMT', 0, 'ADMIN', 'Hà Nội', 'image/jpeg', 'TS'),
('laimanhdung', 'Lại Mạnh Dũng', 0xaf95ff1b7942f5d296d5966e2a7cc897fc0a054af0d9345d8769ecd00b5212096bb0cb3a63f6e24187bfa52b8bbc8356874005ea0cf4bdd76de0577019816cd6, '1981-08-06', '0964978112', 'dunglm@gmail.com', '', '2024-04-10 01:46:01', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0xddaa8a57787a1d1acc58082d6ff11a8b6baeb55496e55c3cc977c3e6191a3fe2a1e1fe20fa06e4d9090d9364f7962b7fdf7633ca211134168ae5dca2369849fb76523eeeeb37ce88d89abf6a8a43ed1747479ee8f862504da121fecaa893b0c7ca30746dc649ab3b3e996b42c87026733b3b9fcc2c70f73ff8cd08d440e89d91, 'dTw+erX3Q8SkJRL39x5SP9pXmlhxMthymNnFXx7QmYnIlBVZL4qa/6jcZkJnhuW/FvF3tQJfSaaBRSN4fhhXvw==', '2024-05-02 10:43:27', '2024-05-09 10:43:27', 'CNPM', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('luongthaile', 'Lương Thái Lê', 0xecc0187c1bb5ac153311dfe1e91dbf833f0bfd265f7d5eb270878410909591e89c9e21b46dfde69b9383f2e6031a7ba22c9588dd7392c7ca43784ea008bc954d, '1980-02-21', '0973223450', 'luongthaile80@gmail.com', '', '2024-04-10 01:49:11', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x8ae02eab6076898cd0446e71ede3f7bb2af17bd231b734b5ab35f8263e286a1a2cb2f8bdd0e62756970b6470fc66db18c074af5934dad2ea67c38197cd7b582f4db1a39c7bd0faa4dded7c08b47f5a47720c405bf42fa07ec062e230dd0fa1d8e104d0e4ae791aa7e4fc515f2750724e3184b110034916fb80eaff29f018abcb, '+w0wWld+yTksWJjHmMAQ3Hygg4kaYPScTQsJah09mJQg1AA42Yt+RvOOCWTe1rP4xF6ZGxN37ZGlJLZ4WnapUQ==', '2024-05-02 10:48:23', '2024-05-09 10:48:23', 'KHMT', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('nguyenhieucuong', 'Nguyễn Hiếu Cường', 0xab088870cffa8e874f206fe542358aaf1a9ace1c0d3b1cf002d2a305358fb23e8fa37c266786d0f0ee0ae2eb358e4cbb5b824c96e98ed43bd12e347b793f830f, '2024-04-10', '0967886712', 'cuonggt@gmail.com', 'fileuser	eacher\nguyenhieucuongavatar_20240426172512.jpg', '2024-04-10 00:44:00', 'hoangvanthong', 'AUTH', 1, 0, NULL, 0xb26a8810b938d23fd890bae2fc60015e274b0f1604d2c2a0581221cad3a4bd960622f3b63c358f3586aa7b36e7813eb679506d4bf59ab0ce52f08445bbc0fe5b98aa9b02607f05138032e8c55b05c5e5794b64af6ff8224b7a9a9bbb82f5d529dd3480947a373915bc596447b90e317d4647b444ce4954e7795f96f894a6ef58, 'XbyFEXRlAV6e+jVDAcXLPH1M6XxQ2ARf/MGRjkGS/aPL4MpqjXv2Vv+aEI5PYWDg2cpcKuAYpbzUU+Hbui+L7Q==', '2024-05-03 10:33:48', '2024-05-10 10:33:48', 'CNPM', 0, 'ADMIN', 'Hà Nội, Việt Nam', 'image/jpeg', 'TS'),
('nguyenkimsao', 'Nguyễn Kim sao', 0xde6c6177e7b8760091106219a9eb875f1fa75587f87665a5fe4644fa2f8f1b0936e45bdef5f4f8d34c77a5adc32fcff88b28c4eb0c1dcdc423db024f43f0dcbc, '1979-12-12', '0905883993', 'saonkoliver@gmail.com', 'fileuser	eacher\nguyenkimsaoavatar_20240425174928.jpg', '2024-04-10 01:44:30', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x55c1baa62260ff25b8c16a0083c20ba37bd2960a39dbc6c14e09e0eb9df71715d9e74faafb3e612f9fc222c20ed340926ca5d14b83c191dde3360f5b7711ac1d84c3cf45b1bfe3fd2a5882dca49ac6f48482284839c4510d8704f874e0d2be0d00dd587962c6c1c1ae81f21c24e53089704d5c86c4747524fab253cfe8079f1f, '5+QMqNW8KAKV0PGZzs3aoAuhvhm+9gizVlIMWRBVAN7NKxGp4zo1dziivcy5eDP70gxxd/P7WeQfa92Wud9nxw==', '2024-04-25 18:05:11', '2024-05-02 18:05:11', 'CNPM', 0, 'TEACHER', 'Hà Nội', 'image/jpeg', 'TS'),
('nguyenquoctuan', 'Nguyễn Đức Dư', 0x6f5e032790420f9d53a4002918445af714588452fc4c3af1e9e24364698bc42248e9c24bd764958e27de5d54a32f5a513753712cef93a6178d6d90d6ef9402ef, '1979-09-14', '0912363245', 'nducdu@gmail.com', '', '2024-04-10 00:48:38', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0xb45b023f7b1d1b72f8f87d1ce57399adbe700e7f023a377bbe0065cd2a7603c8c15f6e96bd73f701fce9b4b25ee2ec739cdec8a94fd45c348aed0c8cadef2ee8536afd6712fb158a1b9e7c18955806906410a62e38feabb2b4d724f4bfd62e4977d39b88be5741efbfa0b8fcd14749e87e35354a7d7540d8dcf8b60dd741c9bc, 'vu409KBNQRXHKt2zpmK2xk1HejKWImZlz5C+LSsDbrq+Olx/bZf2LC4TZooMpa6o/zXaqlZeEtL3MVCqqmFbkA==', '2024-05-02 10:49:37', '2024-05-09 10:49:37', 'KHMT', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('nguyentranhieu', 'Nguyễn Trần Hiếu', 0xf887b5231625fc287742e2d4deb9002cc898cd19e65ef52a0b633c584efcf067b30809a3da665dcbdc63c8d802d5cd922410dac9998338f536b34c6aaf779a57, '1979-11-16', '0912554558', 'nthieu@utc.edu.vn', '', '2024-04-10 01:58:17', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x0dbe7d9767ff341fd0713ad6919b130268959dd4cea4a46036a960f15d94f07bcc7d35913cd90f116cab2ddde264eec100bcfdba01843da65506377068e45a7afe79fbc782ec60e353379b0fc7b3ce7e22ee08ae81f50bbd9fb66a9dbba1dce3dc41d615e9ab6be2a1f547697f8a7aa753e091012900653b5d9cb74a8da5899e, NULL, NULL, NULL, 'MANG', 0, 'TEACHER', 'Hà Nội', NULL, 'ThS'),
('nguyentrongphuc', 'Nguyễn Trọng Phúc', 0x9d9e63e2fe1db51353f7e84c2f54f9106ccc4263900289ece85b843c2c564f1fde168109eda270117e4110fc2af370d84f7f3b988e02f58bbfa25366e415efcf, '1976-10-02', '0936298608', 'Phuc.NguyenTrong@nttdata.com', '', '2024-04-10 01:43:31', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0xa1d383a99dd3e842514072ca620c924344eceea6339b633eac065c789a1fb6e05f8e14a4263d84e3462f1bcd4aa154d9f7b91a9a216d596c20a7b556f7475592c9b3211501e4846a3627ba14f0c968b071377134c309bc4ccb158f2cb7d3fad007b3f183831ecbac053e2488a73560ac8e37cf4537ee1343a225cfe0331a2f3e, NULL, NULL, NULL, 'CNPM', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('nguyenviethung', 'Nguyễn Việt Hưng', 0xa95b1b4746d74ef762ae36f7b56df101a5d128ec104bc47f74566692df4da49596d74c18bdd3b50054b97b5fc8a31bf125ccb71f33caa0e1ab50824e9e7adba3, '1992-05-25', '0868004008', 'hungnv@utc.edu.vn', '', '2024-04-10 01:56:50', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x96f211a065beb043cf37646817051a12fadc6e3be3393100b5a1cfac6a632d60b2a84f4d9ca9a99994144ba753e5a6d343339c413de746fdfddc94ef34dc64da5d2973d9c2ab83580d5af16823375ba63547d548cb6189923c7625f9d5610ab1b151ba80bd5c066387417121c5c31377f066f3687c8a7a6ee7a99b21666ea6c4, NULL, NULL, NULL, 'AI', 0, 'TEACHER', 'Hà Nội', NULL, 'ThS'),
('phamdinhphong', 'Phạm Đình Phong', 0x0e9a67c41a51546a686cf967fca588566bef014164b0e1eb3bcf511dd7f0d8d4051fd9c8e1e2b343bbc07734428fac3e918088b5dc3771de861c47902f366a2f, '1976-09-04', '0972481813', 'phongpd@utc.edu.vn', '', '2024-04-10 01:13:39', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0x9fd40815db27a5054772c041921d08b7bb2bfe1c920e3c0a1c5a3f6bc9c98d4ed53b286929615badc79ecbb1f20f27737cb81b1ee33517894293e15879a7f4ce529883c8a0a2433885ae320f474542d1d3fcbd67a97e18a4ca2d7f281f1efefa6a50b86d842fd2dedb1f9e18f8dede3f0e22bdf2f656d9ac082d99ec1073ebb2, 'Ue+k4nKoMbsdznOyBTqBiT2urqwhcsXSeD8p6VZlwFOayvJPJS76Ocgj/U0//et+FahXJUGqrTPG8w8ynLOp6g==', '2024-05-02 10:51:36', '2024-05-09 10:51:36', 'AI', 0, 'TEACHER', 'Hà Nội', NULL, 'TS'),
('vuhuan', 'Vũ Huấn', 0x7fae2017a17d9cb0f982211021e89f827ae5b6c239b38a75c131add8fc64d702fd9c0046caabe5d7d563ddf201196d569b6d70f42304ae8d24c57142160ed5ca, '1990-11-29', '0988616090', 'huan.vu@utc.edu.vn', '', '2024-04-10 01:11:51', 'hoangvanthong', 'AUTH', 0, 0, NULL, 0xea2cb77fd1fd85cb6dbc6efeff78dfc5a61b989223cbe5f44a76951c88cc23be122326c56f0bc861b4fd620389788ccdc4f23c58bbbd981e23a0c2cc57534312d556a7a8e5b0dad40895fe9e0cc0fb2ceb1020e4a60f501d98e71067e1d0932daa6b54ef1451dd8d0abe5cc4f6d15fb5fbc8bd8378e8ca3e3e8cd58ba0224713, NULL, NULL, NULL, 'AI', 0, 'TEACHER', 'Hà Nội', NULL, 'TS');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `teaching`
--

CREATE TABLE `teaching` (
  `UserNameTeacher` varchar(50) NOT NULL,
  `SemesterId` varchar(50) NOT NULL,
  `GroupReviewOutlineId` varchar(50) DEFAULT NULL,
  `CouncilId` varchar(50) DEFAULT NULL,
  `PositionInCouncil` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `classification`
--
ALTER TABLE `classification`
  ADD PRIMARY KEY (`ClassifiID`);

--
-- Chỉ mục cho bảng `comment`
--
ALTER TABLE `comment`
  ADD PRIMARY KEY (`CommentId`),
  ADD KEY `FK_Comment_ProjectOutline` (`UserName`),
  ADD KEY `FK_Comment_Teacher` (`CreatedBy`);

--
-- Chỉ mục cho bảng `council`
--
ALTER TABLE `council`
  ADD PRIMARY KEY (`CouncilId`),
  ADD KEY `FK_Council_Semester` (`SemesterId`);

--
-- Chỉ mục cho bảng `detailscheduleweek`
--
ALTER TABLE `detailscheduleweek`
  ADD PRIMARY KEY (`ScheduleWeekId`,`UserNameProject`),
  ADD KEY `FK_DetailScheduleWeek_Project` (`UserNameProject`);

--
-- Chỉ mục cho bảng `education`
--
ALTER TABLE `education`
  ADD PRIMARY KEY (`EducationId`);

--
-- Chỉ mục cho bảng `groupreviewoutline`
--
ALTER TABLE `groupreviewoutline`
  ADD PRIMARY KEY (`GroupReviewOutlineId`),
  ADD KEY `FK_GroupReviewOutline_Semester` (`SemesterId`);

--
-- Chỉ mục cho bảng `major`
--
ALTER TABLE `major`
  ADD PRIMARY KEY (`MajorId`);

--
-- Chỉ mục cho bảng `project`
--
ALTER TABLE `project`
  ADD PRIMARY KEY (`UserName`),
  ADD KEY `FK_Project_Council` (`CouncilId`),
  ADD KEY `FK_Project_Semester` (`SemesterId`),
  ADD KEY `FK_Project_Teacher_Commentator` (`UserNameCommentator`),
  ADD KEY `FK_Project_Teacher_Mentor` (`UserNameMentor`);

--
-- Chỉ mục cho bảng `projectoutline`
--
ALTER TABLE `projectoutline`
  ADD PRIMARY KEY (`UserName`),
  ADD KEY `FK_ProjectOutline_GroupReviewOutline` (`GroupReviewOutlineId`);

--
-- Chỉ mục cho bảng `schedulesemester`
--
ALTER TABLE `schedulesemester`
  ADD PRIMARY KEY (`ScheduleSemesterId`),
  ADD KEY `FK_ScheduleSemester_Semester` (`SemesterId`),
  ADD KEY `FK_ScheduleSemester_Teacher` (`CreatedBy`);

--
-- Chỉ mục cho bảng `scheduleweek`
--
ALTER TABLE `scheduleweek`
  ADD PRIMARY KEY (`ScheduleWeekId`),
  ADD KEY `FK_ScheduleWeek_Semester` (`SemesterId`),
  ADD KEY `FK_ScheduleWeek_Teacher` (`CreatedBy`);

--
-- Chỉ mục cho bảng `semester`
--
ALTER TABLE `semester`
  ADD PRIMARY KEY (`SemesterId`),
  ADD KEY `FK_Semester_Teacher` (`CreatedBy`);

--
-- Chỉ mục cho bảng `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`UserName`),
  ADD KEY `FK_Student_Teacher_MentorRegister` (`UserNameMentorRegister`),
  ADD KEY `FK_Student_Major` (`MajorId`);

--
-- Chỉ mục cho bảng `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`UserName`),
  ADD KEY `FK_Teacher_Education` (`EducationId`),
  ADD KEY `FK_Teacher_Major` (`MajorId`);

--
-- Chỉ mục cho bảng `teaching`
--
ALTER TABLE `teaching`
  ADD PRIMARY KEY (`SemesterId`,`UserNameTeacher`),
  ADD KEY `FK_Teaching_Council` (`CouncilId`),
  ADD KEY `FK_Teaching_GroupReviewOutline` (`GroupReviewOutlineId`),
  ADD KEY `FK_Teaching_Teacher` (`UserNameTeacher`);

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `comment`
--
ALTER TABLE `comment`
  ADD CONSTRAINT `FK_Comment_ProjectOutline` FOREIGN KEY (`UserName`) REFERENCES `projectoutline` (`UserName`),
  ADD CONSTRAINT `FK_Comment_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `council`
--
ALTER TABLE `council`
  ADD CONSTRAINT `FK_Council_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`);

--
-- Các ràng buộc cho bảng `detailscheduleweek`
--
ALTER TABLE `detailscheduleweek`
  ADD CONSTRAINT `FK_DetailScheduleWeek_Project` FOREIGN KEY (`UserNameProject`) REFERENCES `project` (`UserName`),
  ADD CONSTRAINT `FK_DetailScheduleWeek_ScheduleWeek` FOREIGN KEY (`ScheduleWeekId`) REFERENCES `scheduleweek` (`ScheduleWeekId`);

--
-- Các ràng buộc cho bảng `groupreviewoutline`
--
ALTER TABLE `groupreviewoutline`
  ADD CONSTRAINT `FK_GroupReviewOutline_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`);

--
-- Các ràng buộc cho bảng `project`
--
ALTER TABLE `project`
  ADD CONSTRAINT `FK_Project_Council` FOREIGN KEY (`CouncilId`) REFERENCES `council` (`CouncilId`),
  ADD CONSTRAINT `FK_Project_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  ADD CONSTRAINT `FK_Project_Student` FOREIGN KEY (`UserName`) REFERENCES `student` (`UserName`),
  ADD CONSTRAINT `FK_Project_Teacher_Commentator` FOREIGN KEY (`UserNameCommentator`) REFERENCES `teacher` (`UserName`),
  ADD CONSTRAINT `FK_Project_Teacher_Mentor` FOREIGN KEY (`UserNameMentor`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `projectoutline`
--
ALTER TABLE `projectoutline`
  ADD CONSTRAINT `FK_ProjectOutline_GroupReviewOutline` FOREIGN KEY (`GroupReviewOutlineId`) REFERENCES `groupreviewoutline` (`GroupReviewOutlineId`),
  ADD CONSTRAINT `FK_ProjectOutline_Project` FOREIGN KEY (`UserName`) REFERENCES `project` (`UserName`);

--
-- Các ràng buộc cho bảng `schedulesemester`
--
ALTER TABLE `schedulesemester`
  ADD CONSTRAINT `FK_ScheduleSemester_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  ADD CONSTRAINT `FK_ScheduleSemester_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `scheduleweek`
--
ALTER TABLE `scheduleweek`
  ADD CONSTRAINT `FK_ScheduleWeek_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  ADD CONSTRAINT `FK_ScheduleWeek_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `semester`
--
ALTER TABLE `semester`
  ADD CONSTRAINT `FK_Semester_Teacher` FOREIGN KEY (`CreatedBy`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `FK_Student_Major` FOREIGN KEY (`MajorId`) REFERENCES `major` (`MajorId`),
  ADD CONSTRAINT `FK_Student_Teacher_MentorRegister` FOREIGN KEY (`UserNameMentorRegister`) REFERENCES `teacher` (`UserName`);

--
-- Các ràng buộc cho bảng `teacher`
--
ALTER TABLE `teacher`
  ADD CONSTRAINT `FK_Teacher_Education` FOREIGN KEY (`EducationId`) REFERENCES `education` (`EducationId`),
  ADD CONSTRAINT `FK_Teacher_Major` FOREIGN KEY (`MajorId`) REFERENCES `major` (`MajorId`);

--
-- Các ràng buộc cho bảng `teaching`
--
ALTER TABLE `teaching`
  ADD CONSTRAINT `FK_Teaching_Council` FOREIGN KEY (`CouncilId`) REFERENCES `council` (`CouncilId`),
  ADD CONSTRAINT `FK_Teaching_GroupReviewOutline` FOREIGN KEY (`GroupReviewOutlineId`) REFERENCES `groupreviewoutline` (`GroupReviewOutlineId`),
  ADD CONSTRAINT `FK_Teaching_Semester` FOREIGN KEY (`SemesterId`) REFERENCES `semester` (`SemesterId`),
  ADD CONSTRAINT `FK_Teaching_Teacher` FOREIGN KEY (`UserNameTeacher`) REFERENCES `teacher` (`UserName`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
