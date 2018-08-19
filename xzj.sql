SET SQL_SAFE_UPDATES = 0;
##用户信息表
create table t_emp
(
	id int not null auto_increment,##唯一标识 自增 
 	e_account varchar(50),##用户名
	e_name varchar(50),##姓名
 	e_sex char(2),##性别
 	e_birth varchar(20),##出生日期
 	e_tel varchar(11),##手机号
 	e_email varchar(20),##邮箱
 	e_address varchar(100),##地址
 	e_pwd varchar(20),##密码
	primary key (id)
)
select 1 from dual;
insert into t_emp(e_account,e_pwd) values('admin','12345678');

delete FROM xzj.t_emp where e_account='admin';


##科室信息表
drop table t_room;


create table t_room
(
	id int not null,
	r_hospital_name varchar(50),##医院名称
	r_address varchar(100),##地址
	r_province varchar(20),##省/直辖市 
	r_rank varchar(20),##等级
	r_postcodes char(6),##邮编
	r_room_name varchar(50),##科室名称
	r_room_tel varchar(11),##科室电话
	r_room_fax varchar(50),##科室传真
	r_responsible varchar(50),##负责人
	r_responsible_title varchar(20),##负责人职称
	r_responsible_tel varchar(11),##负责人电话
	r_responsible_phone varchar(11),##负责人手机
	r_responsible_email varchar(20),##负责人邮箱
	r_dialyse_center_area varchar(10),##透析中心面积
	r_dialyse_unit_area varchar(10),##透析单元面积
	r_start_date varchar(20),##启用日期
	primary key (id)
);

drop table t_dictionary;
##字典管理
create table t_dictionary
(
	id int not null auto_increment,##id为0表示第一级
	d_order varchar(4),##排列序号
	d_parent_id  int,##父id
	d_name varchar(50),##字典名称
	d_desc varchar(500),#描述
	primary key (id)
);

### 
insert into t_dictionary(d_order,d_parent_id,d_name) values(0,0,'医保类型字典');
insert into t_dictionary(d_order,d_parent_id,d_name) values(1,0,'手术字典');
insert into t_dictionary(d_order,d_parent_id,d_name) values(2,0,'情况字典');

#手术字典
insert into t_dictionary(d_parent_id,d_order,d_name) values(2,0,'手术类型字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(2,1,'手术地点字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(2,2,'手术方式字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(2,3,'穿刺方式字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(2,4,'手术追踪期限字典');

#情况字典  
insert into t_dictionary(d_parent_id,d_order,d_name) values(3,0,'内瘘自我锻炼情况字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(3,1,'穿刺部位皮肤情况字典');
insert into t_dictionary(d_parent_id,d_order,d_name) values(3,3,'感染控制方式字典');

### 患者信息表
drop table t_patient;
create table t_patient
(
	id int not null auto_increment,
	p_name varchar(50),##姓名，
	p_sex varchar(2),##性别（选择男/女，
	p_age char(3),##年龄（）根据身份证自动生成，
	p_tel varchar(11),##手机号，（验证）
	p_ID varchar(50),##身份证号
	p_health_type varchar(50),##医保类型（下拉框：字典表-医保类型），
	p_address  varchar(100),##常住地址：省（下拉框），市（下拉框），县区（下拉框），
	p_dialyse_hospital varchar(50),##常析透医院，
	p_dialyse_hospital_contact varchar(50),##常析透医院联系人，
	p_dialyse_hospital_tel varchar(50),##常析透医院联系人电话（验证
	primary key (id)
);


### 手术记录
drop table t_record;
create table t_record
(
	id int not null auto_increment,
	r_patient_ID varchar(50),##患者身份证号
	r_date datetime,##手术日期（自动生成当天时间，精确到分钟）--是否能修改时间？
	r_ss_address varchar(100),##手术地点（科室信息里面的医院名称）--科室信息里面的医院名称都是一样的？
	r_ss_type varchar(50),##手术类型（下拉框：字典表-手术类型）
	r_ss_method varchar(50),##手术方式（下拉框：字典表-手术方式）
	r_cc_method varchar(50),##穿刺方式（下拉框：字典表-穿刺方式）类型
	r_zd_docotor varchar(50),##主刀医生
	r_zs varchar(50),## 助手
	r_qxhs varchar(50),##器械护士
	r_ss_record varchar(2000),##手术记录（文本框，能够调整文本格式和字体）--格式和字体？格式是？字体是（宋体，黑体？）？
	r_is_sszz varchar(4),##是否手术追踪
	primary key (id)
);

SET SQL_SAFE_UPDATES = 0;

select 
	p.p_name,p.p_sex,p.p_age,p.p_tel,p.p_ID,p.p_health_type,p.p_address,
	p.p_dialyse_hospital,p.p_dialyse_hospital_contact,p.p_dialyse_hospital_tel,
	r.r_date,r.r_ss_address,r.r_ss_type,r.r_ss_method,r.r_cc_method,r.r_zd_docotor,
	r.r_zs,r_qxhs,r.r_ss_record,r.r_is_sszz,
	t.t_sszz_deadline,t.t_sfrq,t.t_ccfs,t.t_ssct,t.t_ywxlbct,t.t_ywxm,
	t.t_ywccbwphgmqk,t.t_ywbfz,t.t_ywxbjmqz,t.t_grkzfs,t.t_nwzwdlqk,t.t_ccbwpfqk,
	t.t_sffz,t.t_jmyfw,t.t_sjqbxsjmy,t.t_xjqbxsjmy,t.t_xll,t.t_ypzxsj,t.t_zwcmzcjtzqk,
	t.t_sfys
from t_patient p,t_record r,t_track t
where 
	p.p_ID = r.r_patient_ID
	and r.id = t.t_record_id
	and p.p_ID = '522126186825365632';



### 手术追踪表
drop table t_track;
create table t_track
(
	id int not null auto_increment,
	t_record_id varchar(50),##手术记录id
 	t_sszz_deadline varchar(50),##手术追踪期限(下拉框，在手术字典，医生按照自己的要求添加)
	t_sfrq varchar(20),## 随访日期(自动生成，精确到分)，
	t_ccfs varchar(50),## 穿刺方式(自动生成)，
	t_ssct char(1),##是否通畅，0否 非0是
	t_ywxlbct char(1),##有无血流不畅通，0无 非0有
	t_ywxm char(1),##有无胸闷，0无 非0有
	t_ywccbwphgmqk char(1),##有无穿刺部位皮肤过敏情况，0无 非0有
	t_ywbfz char(1),##有无并发症，0无 非0有
	t_ywxbjmqz char(1),##有无胸壁静脉曲张，0无 非0有
	t_grkzfs varchar(2000),##感染控制方式(自己输入)，
	t_nwzwdlqk varchar(50),##内瘘自我锻炼情况(手术字典)，
	t_ccbwpfqk varchar(50),##穿刺部位皮肤情况(手术字典)，
	t_sffz char(1),##是否复诊，0否 非0是
	t_jmyfw varchar(50),##静脉压范围，
	t_sjqbxsjmy varchar(50),##上机前半小时静脉压，
	t_xjqbxsjmy varchar(50),##下机前半小时静脉压，
	t_xll varchar(50),##血流量，
	t_ypzxsj varchar(20),##压迫止血时间(min)，
	t_zwcmzcjtzqk varchar(2000),##自我触摸震颤及听诊情况(自己输入)，
	t_sfys varchar(50),##随访医生
	primary key (id)
)
 