-- Tạo bảng Classification
use ductmweb_social;
drop database ductmweb_ManagementGraduationProject;
create database ductmweb_ManagementGraduationProject;
use ductmweb_ManagementGraduationProject;

CREATE TABLE Classification (
  ClassifiId VARCHAR(50),
  TypeCode VARCHAR(50) DEFAULT NULL,
  Code VARCHAR(50) DEFAULT NULL,
  Value TEXT,
  Role VARCHAR(50) DEFAULT NULL,
  FileName TEXT,
  Url TEXT,
  CreatedAt DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (ClassifiId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Comment
CREATE TABLE Comment (
  CommentId VARCHAR(50) NOT NULL,
  ContentComment TEXT,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  CreatedDate DATETIME DEFAULT NULL,
  UserName VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (CommentId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Council
CREATE TABLE Council (
  CouncilId VARCHAR(50) NOT NULL,
  CouncilName VARCHAR(100) DEFAULT NULL,
  CouncilZoom VARCHAR(100) DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  SemesterId VARCHAR(50) DEFAULT NULL,
  IsDelete INT DEFAULT NULL,
  CreatedDate DATETIME DEFAULT NULL,
  PRIMARY KEY (CouncilId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng DetailScheduleWeek
CREATE TABLE DetailScheduleWeek (
  ScheduleWeekId VARCHAR(50),
  UserNameProject VARCHAR(50) NOT NULL,
  Comment TEXT,
  NameFile TEXT,
  SizeFile TEXT,
  CreatedDate DATE DEFAULT NULL,
  PRIMARY KEY (ScheduleWeekId, UserNameProject)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Education
CREATE TABLE Education (
  EducationId VARCHAR(50) NOT NULL,
  EducationName TEXT,
  MaxStudentMentor INT DEFAULT NULL,
  CreatedAt DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (EducationId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng GroupReviewOutline
CREATE TABLE GroupReviewOutline (
  GroupReviewOutlineId VARCHAR(50) NOT NULL,
  NameGroupReviewOutline VARCHAR(50) DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  CreatedDate DATETIME DEFAULT NULL,
  SemesterId VARCHAR(50) DEFAULT NULL,
  IsDelete INT DEFAULT NULL,
  PRIMARY KEY (GroupReviewOutlineId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Major cho MySQL
CREATE TABLE Major (
  MajorId VARCHAR(10) NOT NULL,
  MajorName TEXT,
  CreatedAt DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (MajorId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Project cho MySQL
CREATE TABLE Project (
  UserName VARCHAR(50) NOT NULL,
  SemesterId VARCHAR(50) DEFAULT NULL,
  CouncilId VARCHAR(50) DEFAULT NULL,
  UserNameCommentator VARCHAR(50) DEFAULT NULL,
  UserNameMentor VARCHAR(50) DEFAULT NULL,
  ScoreMentor FLOAT DEFAULT NULL,
  CommentMentor TEXT,
  CommentCommentator TEXT,
  ScoreCommentator FLOAT DEFAULT NULL,
  ScoreUV1 FLOAT DEFAULT NULL,
  CommentUV1 TEXT,
  ScoreUV2 FLOAT DEFAULT NULL,
  CommentUV2 TEXT,
  ScoreUV3 FLOAT DEFAULT NULL,
  CommentUV3 TEXT,
  ScoreTK FLOAT DEFAULT NULL,
  CommentTK TEXT,
  ScoreCT FLOAT DEFAULT NULL,
  CommentCT TEXT,
  ScoreFinal FLOAT DEFAULT NULL,
  StatusProject VARCHAR(50) DEFAULT NULL,
  HashKeyMentor TEXT,
  HashKeyCommentator TEXT,
  NameFileFinal TEXT,
  SizeFileFinal TEXT,
  TypeFileFinal TEXT,
  PRIMARY KEY (UserName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng ProjectOutline cho MySQL
CREATE TABLE ProjectOutline (
  UserName VARCHAR(50) NOT NULL,
  NameProject TEXT,
  PlantOutline TEXT,
  TechProject TEXT,
  ExpectResult TEXT,
  ContentProject TEXT,
  GroupReviewOutlineId VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (UserName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng ScheduleSemester cho MySQL
CREATE TABLE ScheduleSemester (
  ScheduleSemesterId VARCHAR(50) NOT NULL,
  FromDate DATETIME DEFAULT NULL,
  ToDate DATETIME DEFAULT NULL,
  TypeSchedule VARCHAR(50) DEFAULT NULL,
  SemesterId VARCHAR(50) DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  CreatedDate DATETIME DEFAULT NULL,
  Title TEXT,
  Implementer VARCHAR(200) DEFAULT NULL,
  Content TEXT,
  Note TEXT,
  StatusSend VARCHAR(10) DEFAULT NULL,
  PRIMARY KEY (ScheduleSemesterId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng ScheduleWeek cho MySQL
CREATE TABLE ScheduleWeek (
  ScheduleWeekId VARCHAR(50) NOT NULL,
  FromDate DATETIME DEFAULT NULL,
  ToDate DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  CreatedDate DATETIME DEFAULT NULL,
  SemesterId VARCHAR(50) DEFAULT NULL,
  Title TEXT,
  Content TEXT,
  PRIMARY KEY (ScheduleWeekId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Semester cho MySQL
CREATE TABLE Semester (
  SemesterId VARCHAR(50) NOT NULL,
  NameSemester VARCHAR(50) DEFAULT NULL,
  FromDate DATE DEFAULT NULL,
  ToDate DATE DEFAULT NULL,
  ScheduleSemesterId VARCHAR(50) DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  IsDelete INT DEFAULT NULL,
  CreatedAt DATETIME DEFAULT NULL,
  PRIMARY KEY (SemesterId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Student cho MySQL
CREATE TABLE Student (
  UserName VARCHAR(50) NOT NULL,
  Password BLOB DEFAULT NULL,
  FullName TEXT,
  DOB DATE DEFAULT NULL,
  Phone VARCHAR(20) DEFAULT NULL,
  Email VARCHAR(50) DEFAULT NULL,
  Avatar TEXT,
  CreatedAt DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  Status VARCHAR(50) DEFAULT NULL,
  StudentCode VARCHAR(30) DEFAULT NULL,
  MajorId VARCHAR(10) DEFAULT NULL,
  ClassName VARCHAR(50) DEFAULT NULL,
  SchoolYearName VARCHAR(50) DEFAULT NULL,
  IsDelete INT DEFAULT NULL,
  Token TEXT,
  PasswordSalt BLOB DEFAULT NULL,
  RefreshToken TEXT,
  TokenCreated DATETIME DEFAULT NULL,
  TokenExpires DATETIME DEFAULT NULL,
  Gender INT DEFAULT NULL,
  GPA FLOAT DEFAULT NULL,
  Address TEXT,
  Role VARCHAR(10) DEFAULT NULL,
  TypeFileAvatar VARCHAR(50) DEFAULT NULL,
  UserNameMentorRegister VARCHAR(50) DEFAULT NULL,
  IsFirstTime INT DEFAULT NULL,
  PRIMARY KEY (UserName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tạo bảng Teacher cho MySQL
CREATE TABLE Teacher (
  UserName VARCHAR(50) NOT NULL,
  FullName TEXT,
  Password BLOB DEFAULT NULL,
  DOB DATE DEFAULT NULL,
  Phone VARCHAR(20) DEFAULT NULL,
  Email VARCHAR(50) DEFAULT NULL,
  Avatar TEXT,
  CreatedAt DATETIME DEFAULT NULL,
  CreatedBy VARCHAR(50) DEFAULT NULL,
  Status VARCHAR(50) DEFAULT NULL,
  IsAdmin INT DEFAULT NULL,
  IsDelete INT DEFAULT NULL,
  Token TEXT,
  PasswordSalt BLOB DEFAULT NULL,
  RefreshToken TEXT,
  TokenCreated DATETIME DEFAULT NULL,
  TokenExpires DATETIME DEFAULT NULL,
  MajorId VARCHAR(10) DEFAULT NULL,
  Gender INT DEFAULT NULL,
  Role VARCHAR(10) DEFAULT NULL,
  Address TEXT,
  TypeFileAvatar VARCHAR(50) DEFAULT NULL,
  EducationId VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (UserName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Tạo bảng Teaching cho MySQL
CREATE TABLE Teaching (
  UserNameTeacher VARCHAR(50) NOT NULL,
  SemesterId VARCHAR(50) NOT NULL,
  GroupReviewOutlineId VARCHAR(50) DEFAULT NULL,
  CouncilId VARCHAR(50) DEFAULT NULL,
  PositionInCouncil VARCHAR(50) DEFAULT NULL,
  PRIMARY KEY (SemesterId, UserNameTeacher)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


ALTER TABLE Classification MODIFY COLUMN ClassifiID VARCHAR(50) DEFAULT (uuid());
ALTER TABLE Classification MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE Comment MODIFY COLUMN CommentId VARCHAR(50) DEFAULT (uuid());
ALTER TABLE Comment MODIFY COLUMN CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE Council MODIFY COLUMN CouncilId VARCHAR(50) DEFAULT (uuid());
ALTER TABLE Council MODIFY COLUMN IsDelete INT DEFAULT 0;
ALTER TABLE Council MODIFY COLUMN CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE DetailScheduleWeek MODIFY COLUMN CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE Education MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE GroupReviewOutline MODIFY COLUMN GroupReviewOutlineId VARCHAR(50) DEFAULT (uuid());
ALTER TABLE GroupReviewOutline MODIFY COLUMN CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE GroupReviewOutline MODIFY COLUMN IsDelete INT DEFAULT 0;

ALTER TABLE Major MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE Project MODIFY COLUMN StatusProject VARCHAR(50) DEFAULT 'START';

ALTER TABLE ScheduleSemester MODIFY COLUMN ScheduleSemesterId VARCHAR(50) DEFAULT (uuid());
ALTER TABLE ScheduleSemester MODIFY COLUMN CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE ScheduleSemester MODIFY COLUMN StatusSend CHAR(1) DEFAULT 'W';

ALTER TABLE ScheduleWeek MODIFY COLUMN ScheduleWeekId VARCHAR(50) DEFAULT (uuid());

ALTER TABLE Semester MODIFY COLUMN IsDelete INT DEFAULT 0;
ALTER TABLE Semester MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE Student MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Student MODIFY COLUMN Status VARCHAR(50) DEFAULT 'AUTH';
ALTER TABLE Student MODIFY COLUMN IsDelete INT DEFAULT 0;
ALTER TABLE Student MODIFY COLUMN Role VARCHAR(50) DEFAULT 'STUDENT';
ALTER TABLE Student MODIFY COLUMN IsFirstTime INT DEFAULT 1;

ALTER TABLE Teacher MODIFY COLUMN CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Teacher MODIFY COLUMN Status VARCHAR(50) DEFAULT 'AUTH';
ALTER TABLE Teacher MODIFY COLUMN IsDelete INT DEFAULT 0;
ALTER TABLE Teacher MODIFY COLUMN Role VARCHAR(50) DEFAULT 'TEACHER';

ALTER TABLE Comment ADD CONSTRAINT FK_Comment_ProjectOutline FOREIGN KEY(UserName) REFERENCES ProjectOutline(UserName);
ALTER TABLE Comment ADD CONSTRAINT FK_Comment_Teacher FOREIGN KEY(CreatedBy) REFERENCES Teacher(UserName);

ALTER TABLE Council ADD CONSTRAINT FK_Council_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);

ALTER TABLE DetailScheduleWeek ADD CONSTRAINT FK_DetailScheduleWeek_Project FOREIGN KEY(UserNameProject) REFERENCES Project(UserName);
ALTER TABLE DetailScheduleWeek ADD CONSTRAINT FK_DetailScheduleWeek_ScheduleWeek FOREIGN KEY(ScheduleWeekId) REFERENCES ScheduleWeek(ScheduleWeekId);

ALTER TABLE GroupReviewOutline ADD CONSTRAINT FK_GroupReviewOutline_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);

ALTER TABLE Project ADD CONSTRAINT FK_Project_Council FOREIGN KEY(CouncilId) REFERENCES Council(CouncilId);
ALTER TABLE Project ADD CONSTRAINT FK_Project_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);
ALTER TABLE Project ADD CONSTRAINT FK_Project_Student FOREIGN KEY(UserName) REFERENCES Student(UserName);
ALTER TABLE Project ADD CONSTRAINT FK_Project_Teacher_Commentator FOREIGN KEY(UserNameCommentator) REFERENCES Teacher(UserName);
ALTER TABLE Project ADD CONSTRAINT FK_Project_Teacher_Mentor FOREIGN KEY(UserNameMentor) REFERENCES Teacher(UserName);

ALTER TABLE ProjectOutline ADD CONSTRAINT FK_ProjectOutline_GroupReviewOutline FOREIGN KEY(GroupReviewOutlineId) REFERENCES GroupReviewOutline(GroupReviewOutlineId);
ALTER TABLE ProjectOutline ADD CONSTRAINT FK_ProjectOutline_Project FOREIGN KEY(UserName) REFERENCES Project(UserName);

ALTER TABLE ScheduleSemester ADD CONSTRAINT FK_ScheduleSemester_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);
ALTER TABLE ScheduleSemester ADD CONSTRAINT FK_ScheduleSemester_Teacher FOREIGN KEY(CreatedBy) REFERENCES Teacher(UserName);

ALTER TABLE ScheduleWeek ADD CONSTRAINT FK_ScheduleWeek_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);
ALTER TABLE ScheduleWeek ADD CONSTRAINT FK_ScheduleWeek_Teacher FOREIGN KEY(CreatedBy) REFERENCES Teacher(UserName);

ALTER TABLE Semester ADD CONSTRAINT FK_Semester_Teacher FOREIGN KEY(CreatedBy) REFERENCES Teacher(UserName);

ALTER TABLE Student ADD CONSTRAINT FK_Student_Teacher_MentorRegister FOREIGN KEY(UserNameMentorRegister) REFERENCES Teacher(UserName);
ALTER TABLE Student ADD CONSTRAINT FK_Student_Major FOREIGN KEY(MajorId) REFERENCES Major(MajorId);

ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_Education FOREIGN KEY(EducationId) REFERENCES Education(EducationId);
ALTER TABLE Teacher ADD CONSTRAINT FK_Teacher_Major FOREIGN KEY(MajorId) REFERENCES Major(MajorId);

ALTER TABLE Teaching ADD CONSTRAINT FK_Teaching_Council FOREIGN KEY(CouncilId) REFERENCES Council(CouncilId);
ALTER TABLE Teaching ADD CONSTRAINT FK_Teaching_GroupReviewOutline FOREIGN KEY(GroupReviewOutlineId) REFERENCES GroupReviewOutline(GroupReviewOutlineId);
ALTER TABLE Teaching ADD CONSTRAINT FK_Teaching_Semester FOREIGN KEY(SemesterId) REFERENCES Semester(SemesterId);
ALTER TABLE Teaching ADD CONSTRAINT FK_Teaching_Teacher FOREIGN KEY(UserNameTeacher) REFERENCES Teacher(UserName);

insert Classification(TypeCode,Code,Value,Role) values('ROLE_SYSTEM','STUDENT','Sinh viên','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('ROLE_SYSTEM','TEACHER','Giảng viên','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('ROLE_SYSTEM','ADMIN','Quản trị viên','ADMIN');

insert Classification(TypeCode,Code,Value,Role) values('STATUS_SYSTEM','AUTH','Hoạt dộng','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_SYSTEM','BLOCK','Đã khóa','ADMIN');

insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','PAUSE','Bảo lưu','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','DOING','Đang làm đồ án','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','ACCEPT','Được bảo vệ','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','INTERN','Chỉ thực tập','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','REJECT','Không được bảo vệ','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('STATUS_PROJECT','START','Mới tạo','ADMIN');

insert Classification(TypeCode,Code,Value,Role) values('TEMPLATE_FILE','REVIEW_MENTOR','Biểu mẫu đánh giá của giảng viên hướng dẫn','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TEMPLATE_FILE','REVIEW_COMMENTATOR','Biểu mẫu đánh giá của giảng viên phản biện','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TEMPLATE_FILE','OUTLINE','Biểu mẫu đề cương đồ án','ADMIN');

insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_NORMAL','Lịch thông báo','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_FOR_OUTLINE','Lịch chốt đề cương','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_FOR_MENTOR','Chốt điểm GVHD','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_FOR_COMMENTATOR','Chốt điểm GVPB','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_FINAL_FILE','Nộp báo cáo cuối cùng','ADMIN');
insert Classification(TypeCode,Code,Value,Role) values('TYPE_SCHEDULE','SCHEDULE_FINAL_SCORE','Chốt điểm tổng kết','ADMIN');

insert Education(EducationId,EducationName,MaxStudentMentor,CreatedBy) values('KS','Kỹ sư',20,'hoangvanthong');
insert Education(EducationId,EducationName,MaxStudentMentor,CreatedBy) values('ThS','Thạc sĩ',25,'hoangvanthong');
insert Education(EducationId,EducationName,MaxStudentMentor,CreatedBy) values('TS','Tiến sĩ',30,'hoangvanthong');

INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'AI', N'Trí tuệ nhân tạo', CAST(N'2024-04-25T08:32:56.613' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'CNPM', N'Công nghệ phần mềm', CAST(N'2024-04-25T08:32:56.613' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'GAME', N'Lập trình game', CAST(N'2024-04-25T08:32:56.613' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'KHMT', N'Khoa học máy tính', CAST(N'2024-04-25T08:32:56.613' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'MANG', N'Mạng và HTTT', CAST(N'2024-04-25T08:32:56.613' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'TESTER', N'Kiểm thử phần mềm', CAST(N'2024-04-25T09:08:10.533' AS DateTime), N'hoangvanthong');
INSERT Major (MajorId, MajorName, CreatedAt, CreatedBy) VALUES (N'ATT', N'An toàn thông tin', CAST(N'2024-04-25T09:08:10.533' AS DateTime), N'hoangvanthong');


INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'buingocdung', N'Bùi Ngọc Dũng', 0x6DE5CA01021C20CEF0464EDB2B55329AC1D8067357ED7B5F4E4E2E277E554C222A4566EE1EC2FF15DAB288C0DFDBFF96B05B7B6F00DB9AF9917D8AB5745F61E1, CAST(N'1977-05-14' AS Date), N'0913045130', N'bndung@gmail.com', N'', CAST(N'2024-04-10T08:47:39.617' AS DateTime), N'hoangvanthong', N'BLOCK', 0, 0, NULL, 0x18392A1CE75508802095D745DE1BF4EA8FF192EB0E6C2CBEED34CCEE623DE51396F4ABB518767D623524ECCFBBD23BEE0F5FBDA238C2739E233E00C21DCB916146B27524F5DE092E9A588904A1AD9FE0294701E7336E6CB5A11B214EAA5079DE31244AA5F88CEE173A46269C97908635D48F85BC2C0D5922D7F314DACF4A5881, NULL, NULL, NULL, N'MANG', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'caothiluyen', N' Cao Thị Luyên', 0x0201F50604C16A76BFC461DE04BEA94D726D45995BEFAF7B006FC478C7596DA347F095113F70E4EFAFE3AFD53B0602AF9C076436D80E9ADBE67AC7148CE5DD1A, CAST(N'1979-04-28' AS Date), N'0912403345', N'caoluyengt@gmail.com', N'', CAST(N'2024-04-10T08:10:38.673' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0xC01CCA3B5FE7B43B85698E2F7898BBCC78E8E6728B4801DDD01B56A1AE4FB6A2D34739CD82E7D078B272253F29F7F61DC01D8BF9E841BC71805CD7C9DC3446AC0DD6CB95E42D98959E790BD02E89ACAD6C2262F34C80B486DEAC497B90AD76E1FF4308E79450B47902777587BAC69FE637E5D3F748B6E9BC196D8CD1F51AA4D3, N'maP0GJ0QTh3RKkypmCZnnjS2SryfNQxxaZ7c2dLoGbOfWvhZKilQHBrYQAuQSo/Nq/cMf0I2CaOziimz3ESUCw==', CAST(N'2024-04-15T17:12:23.640' AS DateTime), CAST(N'2024-04-22T17:12:23.640' AS DateTime), N'CNPM', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'daolethuy', N'Đào Thị Lệ Thủy', 0xB398A3C726ADED1DE213BE975F93E31E90CFE86544DBF1A4F5DF25D98E159D77DFB4CEF201E0A73D362ECE817D0E70410A29B7DA42027EDD3F145354E6BFEB66, CAST(N'1976-01-29' AS Date), N'0946921976', N'thuydtl@utc.edu.vn', N'', CAST(N'2024-04-10T08:09:19.420' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x35B6BF0ABD05731F34F45B9883184166B60495274AD4B68BFBB75FC52B13B6D012F429A707AD9EB9A4D1AA311AF482BA0B0F7074D9F060EE60F1C2BB81209AC4EA4E0195759C0E843ADEBBE17F31908CFB861C7714179D5F901E40921C31CDA86AF984AA5DF33ACDE90D1BF909DC493716B240185A3739B1DC2E597D543D6C20, N'I9oi14j+9lqASZ+o1J1IvpZjtAwNm6I+9GF4hfUjkkna1afCf6uB/jbj6pHhbFjPjtD+iWhYb1tiI6noCDwZMw==', CAST(N'2024-04-15T17:15:21.663' AS DateTime), CAST(N'2024-04-22T17:15:21.663' AS DateTime), N'CNPM', 0, N'TEACHER', N'Hà Nội', NULL, N'ThS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'dinhcongtung', N'Đinh Công Tùng', 0xBB6E3EC39980587B815D419CCA46D930AD88599C5271D12629ECED8C8FB4B00B772D4B61C9849B0CF1B19154FEDB20F644D10483E99A61111F85C02DD9417182, CAST(N'1997-09-26' AS Date), N'0363641589', N'dinhcongtunggtvt@gmail.com', N'', CAST(N'2024-04-10T08:42:14.930' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x7A4B908FF9BBD0EDF3C7AD1BA1645862266A63547499893149805E137963B82510AFEEA152465BCB5EB7DB4BE6CB06D6F823E66A1B3EA31449FCF51B3C535CE7C6985C37EE6234AEB3741518BAE8BFFF144AAB0E00CB2B732E941DD9ACDCF620C4FE4BAE15D8BAAF2C7C745A7BEAD72D41C2752471CC9FE45406803CA1F17BBC, N'PtqNPNcEe89I8MMxdRNhKyLa6bZ9kvxvm3dXDgaCZfrXq/SRakEplcjrmRMftF/Df/RUUK07z8UZmblImBKPSg==', CAST(N'2024-05-02T10:47:17.307' AS DateTime), CAST(N'2024-05-09T10:47:17.307' AS DateTime), N'CNPM', 0, N'TEACHER', N'Hà Nội', NULL, N'ThS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'dovanduc', N'Đỗ Văn Đức', 0x95E583F3148DD22A050E8912367C764886C34A8C279D44532FCF6450ABA0E4A63F38A03301BB4328DC18D31A34E496B0481D699F05D50CC874836EFA3B9B384C, CAST(N'1979-12-23' AS Date), N'0912324873', N'sy@gmail.com', N'', CAST(N'2024-04-10T08:40:24.793' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x31F0F0DCD65BA94AAD775EC2CD711C23F6EE222D710EE6D4376D914ABB6D28573BE854D5E1286A107B19ED7742B8ED373DCF2EEE6AC3B12351C1B659D738B9643CFEFCDB314B5C915407A598333D49DC00917978735E7643C8FC10A21248CF55113F5630FFD6522640064DB136CC13F33530982579096C493B6CC19B3EF6BA8A, NULL, NULL, NULL, N'KHMT', 0, N'TEACHER', N'Hà Nội', NULL, N'ThS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'hoangvanthong', N'Hoàng Văn Thông', 0xC76A830EFCBD94F8C8AAB38ADFA26299BA73558BD4424F1B89C49D925B5445D1AF1615CBAEBF545E169F482C1309A4CAF4ECC095647FCF83B42309E88E74192D, CAST(N'1979-07-18' AS Date), N'0988113676', N'thonggtvt@gmail.com', N'file\user\teacher\hoangvanthong\avatar_20240424215819.jpg', CAST(N'2024-04-10T07:46:17.260' AS DateTime), N'hoangvanthong', N'AUTH', 1, 0, NULL, 0x5BAF990385F1825B7A5AFF588B56130C504B112A99529ED47807452A5ACE70D28820A075C39465A367E48A42E1B7AFEFC4642E9D609976802C7F08486642A8BF6D85E3AD6E84E2A1FB88A29ABAB507CC593B5C3F9E3458817A93DF802277A53F7263804DA3E1C64545710DB9FB363F6343CD6E1AA1F92AAB3FB983590E06DD67, N'm09GtXW1HlKf957IAbJduZKgKjYw9BOGjXYdZxKL1fWGn0UK+SL0FSaMagX2CQtFBa5cz1CDeFWeW6mjCd7/ag==', CAST(N'2024-05-03T11:05:33.140' AS DateTime), CAST(N'2024-05-10T11:05:33.140' AS DateTime), N'KHMT', 0, N'ADMIN', N'Hà Nội', N'image/jpeg', N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'laimanhdung', N'Lại Mạnh Dũng', 0xAF95FF1B7942F5D296D5966E2A7CC897FC0A054AF0D9345D8769ECD00B5212096BB0CB3A63F6E24187BFA52B8BBC8356874005EA0CF4BDD76DE0577019816CD6, CAST(N'1981-08-06' AS Date), N'0964978112', N'dunglm@gmail.com', N'', CAST(N'2024-04-10T08:46:01.923' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0xDDAA8A57787A1D1ACC58082D6FF11A8B6BAEB55496E55C3CC977C3E6191A3FE2A1E1FE20FA06E4D9090D9364F7962B7FDF7633CA211134168AE5DCA2369849FB76523EEEEB37CE88D89ABF6A8A43ED1747479EE8F862504DA121FECAA893B0C7CA30746DC649AB3B3E996B42C87026733B3B9FCC2C70F73FF8CD08D440E89D91, N'dTw+erX3Q8SkJRL39x5SP9pXmlhxMthymNnFXx7QmYnIlBVZL4qa/6jcZkJnhuW/FvF3tQJfSaaBRSN4fhhXvw==', CAST(N'2024-05-02T10:43:27.987' AS DateTime), CAST(N'2024-05-09T10:43:27.987' AS DateTime), N'CNPM', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'luongthaile', N'Lương Thái Lê', 0xECC0187C1BB5AC153311DFE1E91DBF833F0BFD265F7D5EB270878410909591E89C9E21B46DFDE69B9383F2E6031A7BA22C9588DD7392C7CA43784EA008BC954D, CAST(N'1980-02-21' AS Date), N'0973223450', N'luongthaile80@gmail.com', N'', CAST(N'2024-04-10T08:49:11.757' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x8AE02EAB6076898CD0446E71EDE3F7BB2AF17BD231B734B5AB35F8263E286A1A2CB2F8BDD0E62756970B6470FC66DB18C074AF5934DAD2EA67C38197CD7B582F4DB1A39C7BD0FAA4DDED7C08B47F5A47720C405BF42FA07EC062E230DD0FA1D8E104D0E4AE791AA7E4FC515F2750724E3184B110034916FB80EAFF29F018ABCB, N'+w0wWld+yTksWJjHmMAQ3Hygg4kaYPScTQsJah09mJQg1AA42Yt+RvOOCWTe1rP4xF6ZGxN37ZGlJLZ4WnapUQ==', CAST(N'2024-05-02T10:48:23.730' AS DateTime), CAST(N'2024-05-09T10:48:23.730' AS DateTime), N'KHMT', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyenhieucuong', N'Nguyễn Hiếu Cường', 0xAB088870CFFA8E874F206FE542358AAF1A9ACE1C0D3B1CF002D2A305358FB23E8FA37C266786D0F0EE0AE2EB358E4CBB5B824C96E98ED43BD12E347B793F830F, CAST(N'2024-04-10' AS Date), N'0967886712', N'cuonggt@gmail.com', N'file\user\teacher\nguyenhieucuong\avatar_20240426172512.jpg', CAST(N'2024-04-10T07:44:00.700' AS DateTime), N'hoangvanthong', N'AUTH', 1, 0, NULL, 0xB26A8810B938D23FD890BAE2FC60015E274B0F1604D2C2A0581221CAD3A4BD960622F3B63C358F3586AA7B36E7813EB679506D4BF59AB0CE52F08445BBC0FE5B98AA9B02607F05138032E8C55B05C5E5794B64AF6FF8224B7A9A9BBB82F5D529DD3480947A373915BC596447B90E317D4647B444CE4954E7795F96F894A6EF58, N'XbyFEXRlAV6e+jVDAcXLPH1M6XxQ2ARf/MGRjkGS/aPL4MpqjXv2Vv+aEI5PYWDg2cpcKuAYpbzUU+Hbui+L7Q==', CAST(N'2024-05-03T10:33:48.020' AS DateTime), CAST(N'2024-05-10T10:33:48.020' AS DateTime), N'CNPM', 0, N'ADMIN', N'Hà Nội, Việt Nam', N'image/jpeg', N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyenkimsao', N'Nguyễn Kim sao', 0xDE6C6177E7B8760091106219A9EB875F1FA75587F87665A5FE4644FA2F8F1B0936E45BDEF5F4F8D34C77A5ADC32FCFF88B28C4EB0C1DCDC423DB024F43F0DCBC, CAST(N'1979-12-12' AS Date), N'0905883993', N'saonkoliver@gmail.com', N'file\user\teacher\nguyenkimsao\avatar_20240425174928.jpg', CAST(N'2024-04-10T08:44:30.927' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x55C1BAA62260FF25B8C16A0083C20BA37BD2960A39DBC6C14E09E0EB9DF71715D9E74FAAFB3E612F9FC222C20ED340926CA5D14B83C191DDE3360F5B7711AC1D84C3CF45B1BFE3FD2A5882DCA49AC6F48482284839C4510D8704F874E0D2BE0D00DD587962C6C1C1AE81F21C24E53089704D5C86C4747524FAB253CFE8079F1F, N'5+QMqNW8KAKV0PGZzs3aoAuhvhm+9gizVlIMWRBVAN7NKxGp4zo1dziivcy5eDP70gxxd/P7WeQfa92Wud9nxw==', CAST(N'2024-04-25T18:05:11.983' AS DateTime), CAST(N'2024-05-02T18:05:11.983' AS DateTime), N'CNPM', 0, N'TEACHER', N'Hà Nội', N'image/jpeg', N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyenquoctuan', N'Nguyễn Đức Dư', 0x6F5E032790420F9D53A4002918445AF714588452FC4C3AF1E9E24364698BC42248E9C24BD764958E27DE5D54A32F5A513753712CEF93A6178D6D90D6EF9402EF, CAST(N'1979-09-14' AS Date), N'0912363245', N'nducdu@gmail.com', N'', CAST(N'2024-04-10T07:48:38.023' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0xB45B023F7B1D1B72F8F87D1CE57399ADBE700E7F023A377BBE0065CD2A7603C8C15F6E96BD73F701FCE9B4B25EE2EC739CDEC8A94FD45C348AED0C8CADEF2EE8536AFD6712FB158A1B9E7C18955806906410A62E38FEABB2B4D724F4BFD62E4977D39B88BE5741EFBFA0B8FCD14749E87E35354A7D7540D8DCF8B60DD741C9BC, N'vu409KBNQRXHKt2zpmK2xk1HejKWImZlz5C+LSsDbrq+Olx/bZf2LC4TZooMpa6o/zXaqlZeEtL3MVCqqmFbkA==', CAST(N'2024-05-02T10:49:37.853' AS DateTime), CAST(N'2024-05-09T10:49:37.853' AS DateTime), N'KHMT', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyentranhieu', N'Nguyễn Trần Hiếu', 0xF887B5231625FC287742E2D4DEB9002CC898CD19E65EF52A0B633C584EFCF067B30809A3DA665DCBDC63C8D802D5CD922410DAC9998338F536B34C6AAF779A57, CAST(N'1979-11-16' AS Date), N'0912554558', N'nthieu@utc.edu.vn', N'', CAST(N'2024-04-10T08:58:17.953' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x0DBE7D9767FF341FD0713AD6919B130268959DD4CEA4A46036A960F15D94F07BCC7D35913CD90F116CAB2DDDE264EEC100BCFDBA01843DA65506377068E45A7AFE79FBC782EC60E353379B0FC7B3CE7E22EE08AE81F50BBD9FB66A9DBBA1DCE3DC41D615E9AB6BE2A1F547697F8A7AA753E091012900653B5D9CB74A8DA5899E, NULL, NULL, NULL, N'MANG', 0, N'TEACHER', N'Hà Nội', NULL, N'ThS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyentrongphuc', N'Nguyễn Trọng Phúc', 0x9D9E63E2FE1DB51353F7E84C2F54F9106CCC4263900289ECE85B843C2C564F1FDE168109EDA270117E4110FC2AF370D84F7F3B988E02F58BBFA25366E415EFCF, CAST(N'1976-10-02' AS Date), N'0936298608', N'Phuc.NguyenTrong@nttdata.com', N'', CAST(N'2024-04-10T08:43:31.750' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0xA1D383A99DD3E842514072CA620C924344ECEEA6339B633EAC065C789A1FB6E05F8E14A4263D84E3462F1BCD4AA154D9F7B91A9A216D596C20A7B556F7475592C9B3211501E4846A3627BA14F0C968B071377134C309BC4CCB158F2CB7D3FAD007B3F183831ECBAC053E2488A73560AC8E37CF4537EE1343A225CFE0331A2F3E, NULL, NULL, NULL, N'CNPM', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'nguyenviethung', N'Nguyễn Việt Hưng', 0xA95B1B4746D74EF762AE36F7B56DF101A5D128EC104BC47F74566692DF4DA49596D74C18BDD3B50054B97B5FC8A31BF125CCB71F33CAA0E1AB50824E9E7ADBA3, CAST(N'1992-05-25' AS Date), N'0868004008', N'hungnv@utc.edu.vn', N'', CAST(N'2024-04-10T08:56:50.767' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x96F211A065BEB043CF37646817051A12FADC6E3BE3393100B5A1CFAC6A632D60B2A84F4D9CA9A99994144BA753E5A6D343339C413DE746FDFDDC94EF34DC64DA5D2973D9C2AB83580D5AF16823375BA63547D548CB6189923C7625F9D5610AB1B151BA80BD5C066387417121C5C31377F066F3687C8A7A6EE7A99B21666EA6C4, NULL, NULL, NULL, N'AI', 0, N'TEACHER', N'Hà Nội', NULL, N'ThS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'phamdinhphong', N'Phạm Đình Phong', 0x0E9A67C41A51546A686CF967FCA588566BEF014164B0E1EB3BCF511DD7F0D8D4051FD9C8E1E2B343BBC07734428FAC3E918088B5DC3771DE861C47902F366A2F, CAST(N'1976-09-04' AS Date), N'0972481813', N'phongpd@utc.edu.vn', N'', CAST(N'2024-04-10T08:13:39.600' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0x9FD40815DB27A5054772C041921D08B7BB2BFE1C920E3C0A1C5A3F6BC9C98D4ED53B286929615BADC79ECBB1F20F27737CB81B1EE33517894293E15879A7F4CE529883C8A0A2433885AE320F474542D1D3FCBD67A97E18A4CA2D7F281F1EFEFA6A50B86D842FD2DEDB1F9E18F8DEDE3F0E22BDF2F656D9AC082D99EC1073EBB2, N'Ue+k4nKoMbsdznOyBTqBiT2urqwhcsXSeD8p6VZlwFOayvJPJS76Ocgj/U0//et+FahXJUGqrTPG8w8ynLOp6g==', CAST(N'2024-05-02T10:51:36.223' AS DateTime), CAST(N'2024-05-09T10:51:36.223' AS DateTime), N'AI', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');
INSERT Teacher (UserName, FullName, Password, DOB, Phone, Email, Avatar, CreatedAt, CreatedBy, Status, IsAdmin, IsDelete, Token, PasswordSalt, RefreshToken, TokenCreated, tokenExpires, MajorId, Gender, Role, Address, TypeFileAvatar, EducationId) VALUES (N'vuhuan', N'Vũ Huấn', 0x7FAE2017A17D9CB0F982211021E89F827AE5B6C239B38A75C131ADD8FC64D702FD9C0046CAABE5D7D563DDF201196D569B6D70F42304AE8D24C57142160ED5CA, CAST(N'1990-11-29' AS Date), N'0988616090', N'huan.vu@utc.edu.vn', N'', CAST(N'2024-04-10T08:11:51.417' AS DateTime), N'hoangvanthong', N'AUTH', 0, 0, NULL, 0xEA2CB77FD1FD85CB6DBC6EFEFF78DFC5A61B989223CBE5F44A76951C88CC23BE122326C56F0BC861B4FD620389788CCDC4F23C58BBBD981E23A0C2CC57534312D556A7A8E5B0DAD40895FE9E0CC0FB2CEB1020E4A60F501D98E71067E1D0932DAA6B54EF1451DD8D0ABE5CC4F6D15FB5FBC8BD8378E8CA3E3E8CD58BA0224713, NULL, NULL, NULL, N'AI', 0, N'TEACHER', N'Hà Nội', NULL, N'TS');


select * from Education;

select * from Semester;

select * from Student;
select * from Major;

SET SQL_SAFE_UPDATES = 0;


update Project set CouncilId = null;
update Project set UserNameCommentator = null;
update Teaching set CouncilId = null;

update Teaching set PositionInCouncil = null;

update ProjectOutline set GroupReviewOutlineId = null;

update Teaching set GroupReviewOutlineId = null;


select * from DetailScheduleWeek;

select * from Project where UserName = '1_2024_2025_201200007';

insert into ProjectOutline(UserName) select UserName from Project;
UPDATE Project set StatusProject = 'DOING';

-- Demo phan nhom xet duy va hoi dong


UPDATE Student
SET MajorId = 
    CASE 
        WHEN RAND() < 0.11 THEN 'AI'
        WHEN RAND() < 0.22 THEN 'ATT'
        WHEN RAND() < 0.33 THEN 'CNPM'
        WHEN RAND() < 0.44 THEN 'KHMT'
        WHEN RAND() < 0.55 THEN 'MANG'
        WHEN RAND() < 0.66 THEN 'TESTER'
        WHEN RAND() < 0.77 THEN 'GAME'
        ELSE 'CNPM'
    END where UserName in (select UserName from Project where SemesterId = '1_2023_2024');

update Project set StatusProject = 'ACCEPT' where SemesterId = '1_2023_2024' and UserName <> '1_2023_2024_201210096';

INSERT INTO ProjectOutline (UserName)
SELECT (UserName)
FROM Project
WHERE SemesterId = '1_2023_2024' and UserName <> '1_2023_2024_201210096';

-- start

update ProjectOutline set GroupReviewOutlineId = null;
update Project set CouncilId = null;
update Project set UserNameMentor = null;
update Project set UserNameCommentator = null;
update Student set IsFirstTime = 1;
update Student set MajorId = null where UserName = '1_2023_2024_201210096';
update Project set StatusProject = 'START' where UserName = '1_2023_2024_201210096';
DELETE FROM ProjectOutline WHERE UserName = '1_2023_2024_201210096';
DELETE FROM ProjectOutline WHERE UserName like '%2_2023_2024%';
DELETE FROM Project WHERE SemesterId = '2_2023_2024';
DELETE FROM Student WHERE UserName like '%2_2023_2024%';


-- end

-- reset lại file thêm danh sách
ALTER TABLE Project
DROP FOREIGN KEY FK_Project_Student;

update Student set UserName = null where UserName like '%2_2023_2024%';
update Project set UserName = null where SemesterId = '2_2023_2024';



ALTER TABLE Project
ADD CONSTRAINT FK_Project_Student
FOREIGN KEY (UserName) REFERENCES Student(UserName) ON DELETE CASCADE;

ALTER TABLE Project
DROP FOREIGN KEY FK_ProjectOutline_Project;

ALTER TABLE Project
ADD CONSTRAINT FK_ProjectOutline_Project
FOREIGN KEY (UserName) REFERENCES ProjectOutline(UserName) ON DELETE CASCADE;

SET SQL_SAFE_UPDATES = 0;




