/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2016/1/26 16:54:14                           */
/*==============================================================*/


DROP TABLE IF EXISTS CR_DRUGUSE;

DROP TABLE IF EXISTS CR_DRUGEFFECT;

DROP TABLE IF EXISTS CR_GENEEFFECT;

DROP TABLE IF EXISTS CR_HABITS;

DROP TABLE IF EXISTS CTMS_ADM_EXAMINEITEMOPTIONS;

DROP TABLE IF EXISTS CTMS_ADM_EXAMINEITEMS;

DROP TABLE IF EXISTS CTMS_ADM_EXAMINETEMPLATES;

DROP TABLE IF EXISTS CTMS_BASETEMPLATERESULT;

DROP TABLE IF EXISTS CTMS_CONDITIONITEM;

DROP TABLE IF EXISTS CTMS_FLOWTEMPLATE;

DROP TABLE IF EXISTS CTMS_GUIDELINE;

DROP TABLE IF EXISTS CTMS_GUIDELINEDATA;

DROP TABLE IF EXISTS CTMS_GUIDELINEFLOW;

DROP TABLE IF EXISTS CTMS_IMAGEEXAMINEREPORT;

DROP TABLE IF EXISTS CTMS_METADATA;

DROP TABLE IF EXISTS CTMS_METADATAPARAM;

DROP TABLE IF EXISTS CTMS_SYS_FUNCTION;

DROP TABLE IF EXISTS CTMS_SYS_PERMISSION;

DROP TABLE IF EXISTS CTMS_SYS_ROLE;

DROP TABLE IF EXISTS CTMS_SYS_ROLEFUNCTION;

DROP TABLE IF EXISTS CTMS_SYS_USERROLE;

DROP TABLE IF EXISTS CTMS_SERVICETRACEINFO;

DROP TABLE IF EXISTS CTMS_USERAPPLY;

DROP TABLE IF EXISTS CTMS_USEREVENT;

DROP TABLE IF EXISTS DUG_CNDRUG;

DROP TABLE IF EXISTS DUG_DRUG;

DROP TABLE IF EXISTS DUG_DRUG3;

DROP TABLE IF EXISTS DUG_DRUGINTERACTIONS;

DROP TABLE IF EXISTS DUG_DRUGSYNONYMS;

DROP TABLE IF EXISTS DUG_ENZYMEPO;

DROP TABLE IF EXISTS DUG_ENZYMES;

DROP TABLE IF EXISTS DUG_ENZYMESACTIONS;

DROP TABLE IF EXISTS DUG_ENZYMESIDENTIFIER;

DROP TABLE IF EXISTS DUG_ENZYMESSYNONYMS;

DROP TABLE IF EXISTS DUG_EXTERNALIDENTIFIERS;

DROP TABLE IF EXISTS DUG_PRODUCTS;

DROP TABLE IF EXISTS DUG_TARGETACTIONS;

DROP TABLE IF EXISTS DUG_TARGETIDENTIFIER;

DROP TABLE IF EXISTS DUG_TARGETS;

DROP TABLE IF EXISTS DUG_TARGETSPO;

DROP TABLE IF EXISTS DUG_TRANSPORTERACTIONS;

DROP TABLE IF EXISTS DUG_TRANSPORTERIDENTIFIER;

DROP TABLE IF EXISTS DUG_TRANSPORTERS;

DROP TABLE IF EXISTS DUG_TRANSPORTERSPO;

DROP TABLE IF EXISTS GN_DRUGALLELE;

DROP TABLE IF EXISTS GN_GENE;

DROP TABLE IF EXISTS GN_GENEALLELE;

DROP TABLE IF EXISTS GN_GENEALLELELOCUS;

DROP TABLE IF EXISTS HPN_BODYTYPE;

DROP TABLE IF EXISTS HPN_MIGRATIONHISTORY;

DROP TABLE IF EXISTS HPN_RESULTDESC;

DROP TABLE IF EXISTS HPN_SENDVERCODELOG;

DROP TABLE IF EXISTS HPN_STANDARDADVISE;

DROP TABLE IF EXISTS HPN_SUBRESULTDESC;

DROP TABLE IF EXISTS HPN_TEMPLATE;

DROP TABLE IF EXISTS HPN_TEMPLATEITEM;

DROP TABLE IF EXISTS HPN_TEMPLATEITEMOPTIONS;

DROP TABLE IF EXISTS HPN_TEMPLATEITEMRESULT;

DROP TABLE IF EXISTS HPN_TEMPLATETYPE;

DROP TABLE IF EXISTS HPN_TESTRESULT;

DROP TABLE IF EXISTS HPN_TESTRESULTDETAILS;

DROP TABLE IF EXISTS HPN_USERTYPE;

DROP TABLE IF EXISTS HPN_USERS;

DROP TABLE IF EXISTS HPN_VIPDESC;

DROP TABLE IF EXISTS HR_ACTIVITYLOG;

DROP TABLE IF EXISTS HR_ANNUALINCOME;

DROP TABLE IF EXISTS HR_AREA;

DROP TABLE IF EXISTS HR_ATTACHMENT;

DROP TABLE IF EXISTS HR_CNR_USER;

DROP TABLE IF EXISTS HR_DICTIONARY;

DROP TABLE IF EXISTS HR_DISEASEHISTORY;

DROP TABLE IF EXISTS HR_EXAMINEITEMS;

DROP TABLE IF EXISTS HR_EXAMINERECORD;

DROP TABLE IF EXISTS HR_EXAMINERESULT;

DROP TABLE IF EXISTS HR_EXAMINETEMPLATE;

DROP TABLE IF EXISTS HR_EXAMINETEMPLATES;

DROP TABLE IF EXISTS HR_ENTITYRELATION;

DROP TABLE IF EXISTS HR_FD_FIMILYMEMBER;

DROP TABLE IF EXISTS HR_FD_FIMILYMEMBERDISEASE;

DROP TABLE IF EXISTS HR_FD_FIMILYRELATION;

DROP TABLE IF EXISTS HR_FAMILY;

DROP TABLE IF EXISTS HR_FAMILYMEMBER;

DROP TABLE IF EXISTS HR_FILEUPLOAD;

DROP TABLE IF EXISTS HR_ITEMSTANDVAL;

DROP TABLE IF EXISTS HR_IMAGEEXAMINATION;

DROP TABLE IF EXISTS HR_LABORATORYRESULT;

DROP TABLE IF EXISTS HR_LABORATORYTESTITEM;

DROP TABLE IF EXISTS HR_PERSONINFO;

DROP TABLE IF EXISTS HR_REF_CODEFILE;

DROP TABLE IF EXISTS HR_SYS_USER;

DROP TABLE IF EXISTS HR_SCORE;

DROP TABLE IF EXISTS HR_SCORETEMPLATE;

DROP TABLE IF EXISTS HR_SEEDOCHISICD;

DROP TABLE IF EXISTS HR_SEEDOCTORHISTORY;

DROP TABLE IF EXISTS HR_TEMPLATEITEM;

DROP TABLE IF EXISTS HR_TREATMENTHISTORY;

DROP TABLE IF EXISTS HR_USERGENE;

DROP TABLE IF EXISTS ICD10;

/*==============================================================*/
/* Table: CR_DRUGUSE                                            */
/*==============================================================*/
CREATE TABLE CR_DRUGUSE
(
   ID                   INT NOT NULL COMMENT 'ID',
   DISEASEID            VARCHAR(50) COMMENT '病例ID',
   USERID               VARCHAR(50) COMMENT '用户Id',
   DRUGTYPE             VARCHAR(50) COMMENT '药典类型',
   DRUGNAME             VARCHAR(100) COMMENT '药品名称',
   DRUGID               VARCHAR(50) COMMENT '药品编号',
   TREATMENTDISEASES    VARCHAR(200) COMMENT '治疗疾病',
   STARTTIME            DATETIME COMMENT '开始服用时间',
   ENDTIME              DATETIME COMMENT '结束服用时间',
   TIMESADAY            VARCHAR(20) COMMENT '每天服用次数',
   DOSE                 VARCHAR(100) COMMENT '剂量',
   UNITS                VARCHAR(50) COMMENT '单位',
   USEDAY               INT COMMENT '已使用天数',
   ISNEWDRUG            CHAR(1) COMMENT '是否新用药',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            CHAR(1) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (ID)
);

ALTER TABLE CR_DRUGUSE COMMENT '用药情况';

/*==============================================================*/
/* Table: CR_DRUGEFFECT                                         */
/*==============================================================*/
CREATE TABLE CR_DRUGEFFECT
(
   ID                   INT NOT NULL COMMENT 'ID',
   DISEASEID            VARCHAR(50) COMMENT '用药情况Id',
   DRUGID               VARCHAR(50) COMMENT '药物ID',
   INTERDRUGID          VARCHAR(50) COMMENT '相互作用药物ID',
   DRUGINTERACTION      VARCHAR(2000) COMMENT '药物相互作用',
   PRIMARY KEY (ID)
);

ALTER TABLE CR_DRUGEFFECT COMMENT '药物相互作用';

/*==============================================================*/
/* Table: CR_GENEEFFECT                                         */
/*==============================================================*/
CREATE TABLE CR_GENEEFFECT
(
   ID                   INT NOT NULL COMMENT 'ID',
   DISEASEID            VARCHAR(50) COMMENT '用药情况Id',
   DRUGID               VARCHAR(50) COMMENT '药物ID',
   GENENAME             VARCHAR(200) COMMENT '影响的Gene名称',
   GENEDESC             VARCHAR(2000) COMMENT '影响的Gene描述',
   PRIMARY KEY (ID)
);

ALTER TABLE CR_GENEEFFECT COMMENT '药物与基因的作用';

/*==============================================================*/
/* Table: CR_HABITS                                             */
/*==============================================================*/
CREATE TABLE CR_HABITS
(
   ID                   INT NOT NULL COMMENT 'ID',
   DISEASEID            VARCHAR(50) COMMENT '用药情况ID',
   HABITSCODE           VARCHAR(50) COMMENT '生活习惯',
   PRIMARY KEY (ID)
);

ALTER TABLE CR_HABITS COMMENT '生活习惯';

/*==============================================================*/
/* Table: CTMS_ADM_EXAMINEITEMOPTIONS                           */
/*==============================================================*/
CREATE TABLE CTMS_ADM_EXAMINEITEMOPTIONS
(
   ID                   VARCHAR(100) NOT NULL COMMENT '选项id',
   EXAMINEITEMID        VARCHAR(100) COMMENT '检验项id',
   DESCRIPTION          VARCHAR(255) COMMENT '描述',
   CREATEUSERID         VARCHAR(100) COMMENT '创建者id',
   CREATEUSERNAME       VARCHAR(100) COMMENT '创建者名称',
   CREATEDATETIME       DATETIME COMMENT '创建日期',
   EDITUSERID           VARCHAR(100) COMMENT '修改者id',
   EDITUSERNAME         VARCHAR(100) COMMENT '修改者名称',
   EDITDATETIME         DATETIME COMMENT '修改日期',
   ISDELETED            NUMERIC(8,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_ADM_EXAMINEITEMOPTIONS COMMENT '检验项选项';

/*==============================================================*/
/* Table: CTMS_ADM_EXAMINEITEMS                                 */
/*==============================================================*/
CREATE TABLE CTMS_ADM_EXAMINEITEMS
(
   ID                   VARCHAR(100) NOT NULL COMMENT '主键id',
   EXAMINETEMPLATEID    VARCHAR(100) COMMENT '检验模版id',
   NAME                 VARCHAR(100) COMMENT '检验项名称',
   TYPE                 NUMERIC(8,0) COMMENT '检验项类型',
   CREATEUSERID         VARCHAR(100) COMMENT '创建者id',
   CREATEUSERNAME       VARCHAR(100) COMMENT '创建者名称',
   CREATEDATETIME       DATETIME COMMENT '创建日期',
   EDITUSERID           VARCHAR(100) COMMENT '修改者id',
   EDITUSERNAME         VARCHAR(100) COMMENT '修改者名称',
   EDITDATETIME         DATETIME COMMENT '修改日期',
   ISDELETED            NUMERIC(8,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_ADM_EXAMINEITEMS COMMENT '检验项';

/*==============================================================*/
/* Table: CTMS_ADM_EXAMINETEMPLATES                             */
/*==============================================================*/
CREATE TABLE CTMS_ADM_EXAMINETEMPLATES
(
   ID                   VARCHAR(100) NOT NULL COMMENT 'ID',
   NAME                 VARCHAR(100) COMMENT '检验模版名称',
   DESCRIPTION          VARCHAR(255) COMMENT '检验模版描述',
   CREATEUSERID         VARCHAR(100) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(100) COMMENT '创建者名称',
   CREATEDATETIME       DATETIME COMMENT '创建日期',
   EDITUSERID           VARCHAR(100) COMMENT '修改者id',
   EDITUSERNAME         VARCHAR(100) COMMENT '修改者名称',
   EDITDATETIME         DATETIME COMMENT '修改日期',
   ISDELETED            NUMERIC(8,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_ADM_EXAMINETEMPLATES COMMENT '检验模版';

/*==============================================================*/
/* Table: CTMS_BASETEMPLATERESULT                               */
/*==============================================================*/
CREATE TABLE CTMS_BASETEMPLATERESULT
(
   BASETEMPLATEID       VARCHAR(50) NOT NULL COMMENT '依据模板ID',
   HISTORYID            VARCHAR(50) NOT NULL COMMENT '就诊史ID',
   EXAMINETEMPLATESID   VARCHAR(50) COMMENT '所使用依据模板ID',
   RESULT               VARCHAR(4000) COMMENT '结果明细',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            CHAR(1) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (BASETEMPLATEID)
);

ALTER TABLE CTMS_BASETEMPLATERESULT COMMENT '依据模板检验结果';

/*==============================================================*/
/* Table: CTMS_CONDITIONITEM                                    */
/*==============================================================*/
CREATE TABLE CTMS_CONDITIONITEM
(
   ID                   NUMERIC(9,0) NOT NULL COMMENT 'ID',
   CONDTYPE             VARCHAR(50) COMMENT '类别',
   DISPLAYNAME          VARCHAR(200) COMMENT '显示名称',
   DATATYPE             NUMERIC(9,0) COMMENT '数据类型',
   CATEGORY             VARCHAR(50) COMMENT '分类',
   METADATAID           NUMERIC(9,0) COMMENT '元数据ID',
   OPERATOR             NUMERIC(9,0) COMMENT '运算符',
   OPERATEVALUE         VARCHAR(200) COMMENT '比较值',
   LOGICALOPERATOR      NUMERIC(9,0) COMMENT '逻辑运算符',
   CONDIDS              VARCHAR(200) COMMENT '条件项IDs',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除',
   USERID               VARCHAR(100) COMMENT '用户(患者)',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_CONDITIONITEM COMMENT '条件项';

/*==============================================================*/
/* Table: CTMS_FLOWTEMPLATE                                     */
/*==============================================================*/
CREATE TABLE CTMS_FLOWTEMPLATE
(
   ID                   VARCHAR(50) COMMENT 'ID',
   GUIDELINEID          VARCHAR(50) COMMENT '临床路径ID',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除'
);

ALTER TABLE CTMS_FLOWTEMPLATE COMMENT '流程模板';

/*==============================================================*/
/* Table: CTMS_GUIDELINE                                        */
/*==============================================================*/
CREATE TABLE CTMS_GUIDELINE
(
   ID                   VARCHAR(50) COMMENT 'ID',
   CODE                 VARCHAR(50) COMMENT '编码',
   NAME                 VARCHAR(200) COMMENT '名称',
   PARENTID             VARCHAR(50) COMMENT '父临床路径ID',
   ISINHERIT            NUMERIC(1,0) COMMENT '是否继承父路径进入条件',
   CLINICALBASIS        VARCHAR(2000) COMMENT '临床依据',
   ENTERLOGICALOPERATOR NUMERIC(9,0) COMMENT '进入条件逻辑运算符',
   ENTERCONDITEMIDS     VARCHAR(200) COMMENT '进入条件项IDs',
   OUTLOGICALOPERATOR   NUMERIC(9,0) COMMENT '退出条件运算符',
   OUTCONDITEMIDS       VARCHAR(200) COMMENT '退出条件IDs',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除'
);

ALTER TABLE CTMS_GUIDELINE COMMENT '临床路径';

/*==============================================================*/
/* Table: CTMS_GUIDELINEDATA                                    */
/*==============================================================*/
CREATE TABLE CTMS_GUIDELINEDATA
(
   ID                   VARCHAR(50) COMMENT 'ID',
   GUIDELINEID          VARCHAR(50) COMMENT '临床路径ID',
   TEXT                 VARCHAR(200) COMMENT '属性',
   VALUE                VARCHAR(2000) COMMENT '值',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除'
);

ALTER TABLE CTMS_GUIDELINEDATA COMMENT '临床路径元数据';

/*==============================================================*/
/* Table: CTMS_GUIDELINEFLOW                                    */
/*==============================================================*/
CREATE TABLE CTMS_GUIDELINEFLOW
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   GUIDELINENAME        VARCHAR(100) COMMENT 'GuideLine流程名称',
   GUIDELINEINFO        VARCHAR(4000) COMMENT 'GuideLine内容',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            NUMERIC(1,0) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_GUIDELINEFLOW COMMENT 'GuideLine路劲';

/*==============================================================*/
/* Table: CTMS_IMAGEEXAMINEREPORT                               */
/*==============================================================*/
CREATE TABLE CTMS_IMAGEEXAMINEREPORT
(
   ID                   VARCHAR(100) NOT NULL COMMENT 'id',
   TEMPLATETYPE         VARCHAR(100) COMMENT '报告单类型',
   CHECKDATE            DATETIME COMMENT '检查日期',
   DEPT                 VARCHAR(100) COMMENT '申请科室',
   PART                 VARCHAR(100) COMMENT '检查部位',
   DIAGNOSE             VARCHAR(255) COMMENT '临床诊断',
   SEE                  VARCHAR(255) COMMENT '检查所见',
   CREATEDATETIME       DATETIME COMMENT '创建日期',
   CREATEUSERID         VARCHAR(100) COMMENT '创建者id',
   CREATEUSERNAME       VARCHAR(100) COMMENT '创建者名称',
   EDITDATETIME         DATETIME COMMENT '修改日期',
   EDITUSERID           VARCHAR(100) COMMENT '修改者id',
   EDITUSERNAME         VARCHAR(100) COMMENT '修改者名称',
   ISDELETED            NUMERIC(8,0) COMMENT '是否删除',
   REPORTDOCTOR         VARCHAR(100) COMMENT '报告医生',
   CHECKDOCTOR          VARCHAR(100) COMMENT '审核医生',
   REPORTDETAIL         LONGBLOB COMMENT '报告明细',
   REMARK               VARCHAR(255) COMMENT '备注',
   USERID               VARCHAR(100) COMMENT '用户(患者)',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_IMAGEEXAMINEREPORT COMMENT '影像检验报告';

/*==============================================================*/
/* Table: CTMS_METADATA                                         */
/*==============================================================*/
CREATE TABLE CTMS_METADATA
(
   ID                   NUMERIC(9,0) NOT NULL COMMENT 'ID',
   CATEGORY             VARCHAR(50) COMMENT '分类',
   DISPLAYNAME          VARCHAR(200) COMMENT '显示名称',
   DATATYPE             NUMERIC(9,0) COMMENT '数据类型',
   DATASOURCETYPE       NUMERIC(9,0) COMMENT '数据来源类型',
   DATASOURCE           VARCHAR(50) COMMENT '数据来源',
   DATASOURCECOLUMN     VARCHAR(50) COMMENT '数据来源列',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_METADATA COMMENT '元数据';

/*==============================================================*/
/* Table: CTMS_METADATAPARAM                                    */
/*==============================================================*/
CREATE TABLE CTMS_METADATAPARAM
(
   ID                   NUMERIC(9,0) NOT NULL COMMENT 'ID',
   METADATAID           NUMERIC(9,0) COMMENT '元数据ID',
   PARAMNAME            VARCHAR(50) COMMENT '参数名',
   PARAMVALUE           VARCHAR(50) COMMENT '参数值',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_METADATAPARAM COMMENT '元数据参数';

/*==============================================================*/
/* Table: CTMS_SYS_FUNCTION                                     */
/*==============================================================*/
CREATE TABLE CTMS_SYS_FUNCTION
(
   FUNCTIONID           VARCHAR(50) NOT NULL COMMENT '实体菜单ID',
   ENTITYCODE           VARCHAR(200) COMMENT '实体编码',
   ENTITYNAME           VARCHAR(200) COMMENT '实体名称',
   STATUS               NUMERIC(9,0) NOT NULL DEFAULT 0 COMMENT '状态',
   SYSTEMCATEGORY       NUMERIC(9,0) NOT NULL DEFAULT 0 COMMENT '系统类型',
   PARENTID             VARCHAR(50) COMMENT '父功能ID',
   ISMENU               NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否有系统菜单',
   MENUCODE             VARCHAR(50) COMMENT '菜单编码',
   MENUNAME             VARCHAR(50) COMMENT '菜单名称',
   MENUICON             VARCHAR(50) COMMENT '菜单Icon',
   MENUURL              VARCHAR(2000) COMMENT '菜单Url',
   ISEXPAND             VARCHAR(100) COMMENT '菜单图标地址',
   HELPERTITLE          VARCHAR(100) COMMENT '帮助标题',
   HELPERURL            VARCHAR(500) COMMENT '帮助文件地址',
   SORT                 NUMERIC(9,0) NOT NULL DEFAULT 0 COMMENT '菜单序号',
   REMARK               VARCHAR(2000) COMMENT '备注：0 hr  1 oa 2物流 3fb 6 资产 7 权限 8 流程  9引擎 10采购 13日常管理',
   ISPUBLIC             NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否受权限控制',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间2',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间2',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (FUNCTIONID)
);

ALTER TABLE CTMS_SYS_FUNCTION COMMENT '系统实体菜单';

/*==============================================================*/
/* Table: CTMS_SYS_PERMISSION                                   */
/*==============================================================*/
CREATE TABLE CTMS_SYS_PERMISSION
(
   PERMISSIONID         VARCHAR(50) NOT NULL COMMENT '权限ID',
   PERMISSIONNAME       VARCHAR(50) NOT NULL COMMENT '权限名称',
   PERMISSIONVALUE      NUMERIC(9,0) NOT NULL DEFAULT 0 COMMENT '权限值',
   PERMISSIONCODE       VARCHAR(50) COMMENT '权限编码',
   REMARK               VARCHAR(2000) COMMENT '备注',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间2',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间2',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (PERMISSIONID)
);

ALTER TABLE CTMS_SYS_PERMISSION COMMENT '操作权限
增,删,改,查,导入,导出,报表,打印,共享数据....';

/*==============================================================*/
/* Table: CTMS_SYS_ROLE                                         */
/*==============================================================*/
CREATE TABLE CTMS_SYS_ROLE
(
   ROLEID               VARCHAR(50) NOT NULL COMMENT '角色ID',
   SYSTEMCATEGORY       NUMERIC(9,0) COMMENT '系统类型',
   ROLENAME             VARCHAR(50) COMMENT '角色名称',
   REMARK               VARCHAR(2000) COMMENT '备注',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (ROLEID)
);

ALTER TABLE CTMS_SYS_ROLE COMMENT '角色';

/*==============================================================*/
/* Table: CTMS_SYS_ROLEFUNCTION                                 */
/*==============================================================*/
CREATE TABLE CTMS_SYS_ROLEFUNCTION
(
   ROLEFUNCTIONID       VARCHAR(50) NOT NULL COMMENT '角色实体菜单ID',
   ROLEID               VARCHAR(50) COMMENT '角色ID',
   FUNCTIONID           VARCHAR(50) COMMENT '功能ID',
   PERMISSIONVALUE      NUMERIC(9,0) NOT NULL DEFAULT 0 COMMENT '权限值',
   DATARANGE            VARCHAR(2000) COMMENT '范围',
   REMARK               VARCHAR(2000) COMMENT '备注',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间2',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间2',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (ROLEFUNCTIONID)
);

ALTER TABLE CTMS_SYS_ROLEFUNCTION COMMENT '角色实体菜单';

/*==============================================================*/
/* Table: CTMS_SYS_USERROLE                                     */
/*==============================================================*/
CREATE TABLE CTMS_SYS_USERROLE
(
   USERROLEID           VARCHAR(50) NOT NULL COMMENT '用户角色ID',
   USERID               VARCHAR(50) COMMENT '用户ID',
   ROLEID               VARCHAR(50) COMMENT '角色ID',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) DEFAULT 0 COMMENT '是否删除',
   PRIMARY KEY (USERROLEID)
);

ALTER TABLE CTMS_SYS_USERROLE COMMENT '用户角色';

/*==============================================================*/
/* Table: CTMS_SERVICETRACEINFO                                 */
/*==============================================================*/
CREATE TABLE CTMS_SERVICETRACEINFO
(
   ID                   VARCHAR(100) NOT NULL COMMENT '主键id',
   TRACETYPE            NUMERIC(8,0) COMMENT '追踪方式',
   TRACEDETAIL          VARCHAR(1000) COMMENT '追踪详情',
   CREATEDATETIME       DATETIME COMMENT '创建日期',
   CREATEUSERID         VARCHAR(100) COMMENT '创建者id',
   CREATEUSERNAME       VARCHAR(100) COMMENT '创建者名称',
   FOREVENTID           VARCHAR(100) COMMENT '事件id',
   ISVALID              NUMERIC(8,0) COMMENT '是否有效',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_SERVICETRACEINFO COMMENT '客服追踪信息表';

/*==============================================================*/
/* Table: CTMS_USERAPPLY                                        */
/*==============================================================*/
CREATE TABLE CTMS_USERAPPLY
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   APPLYID              VARCHAR(50) COMMENT '规范实例ID',
   USERID               VARCHAR(50) COMMENT '用户ID',
   GUIDELINEID          VARCHAR(50) COMMENT '肿瘤GuideLine定义ID',
   CURRENTNODE          VARCHAR(50) COMMENT '当前结点ID',
   STATUS               VARCHAR(50) COMMENT '节点状态',
   DOCTORSUGGEST        VARCHAR(1000) COMMENT '医生建议',
   ENTRYDATE            DATETIME COMMENT '进入日期',
   EXITDATE             DATETIME COMMENT '退出日期',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            NUMERIC(1,0) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (ID)
);

ALTER TABLE CTMS_USERAPPLY COMMENT '规范实例表';

/*==============================================================*/
/* Table: CTMS_USEREVENT                                        */
/*==============================================================*/
CREATE TABLE CTMS_USEREVENT
(
   EVENTID              VARCHAR(50) NOT NULL COMMENT '代办事项ID',
   APPLYID              VARCHAR(50) COMMENT '规范实例ID',
   ACTIONTYPE           VARCHAR(20) COMMENT '待办事项类型',
   ACTIONINFO           VARCHAR(200) COMMENT '待办任务',
   RECEIPTTIME          DATETIME COMMENT '接收时间',
   ENDTIME              DATETIME COMMENT '完成时间',
   ACTIONSTATUS         NUMERIC(1,0) COMMENT '待办状态',
   FROMUSER             VARCHAR(50) COMMENT '发送者',
   TOUSER               VARCHAR(50) COMMENT '接收者',
   TOUSERTYPE           VARCHAR(50) COMMENT '接收者类型',
   CREATETIME           DATETIME COMMENT '创建时间',
   MODELTYPE            VARCHAR(50) COMMENT '单据类型',
   MODELID              VARCHAR(50) COMMENT '单据ID',
   REMARKS              VARCHAR(2000) COMMENT '处理说明',
   LINKURL              VARCHAR(100) COMMENT '链接地址',
   PRIMARY KEY (EVENTID)
);

ALTER TABLE CTMS_USEREVENT COMMENT '代办事项';

/*==============================================================*/
/* Table: DUG_CNDRUG                                            */
/*==============================================================*/
CREATE TABLE DUG_CNDRUG
(
   ID                   INT NOT NULL COMMENT '主键',
   IDENTIFIER           VARCHAR(100) COMMENT '编号',
   NAME                 VARCHAR(500) COMMENT '药品名称',
   PINYINCODE           VARCHAR(100) COMMENT '拼音码',
   ENNAME               VARCHAR(100) COMMENT '英文名称',
   COMMONNAME           VARCHAR(100) COMMENT '同义词',
   TYPENAME             VARCHAR(100) COMMENT '类型',
   KINDNAME             VARCHAR(100) COMMENT '种类',
   ISPRESCRIPTION       NUMERIC(1,0) COMMENT '是否处方药',
   ISMEDICALINSURANCE   NUMERIC(1,0) COMMENT '是否医保用药',
   COMPANY              VARCHAR(1000) COMMENT '生产厂家',
   PACK                 VARCHAR(1000) COMMENT '规格',
   DOSAGEFORMS          VARCHAR(100) COMMENT '剂型',
   DOSAGE               VARCHAR(2000) COMMENT '用量用法',
   DESCRIPTION          VARCHAR(4000) COMMENT '描述',
   INDICATION           VARCHAR(4000) COMMENT '主治功能',
   CONTENT              TEXT COMMENT '明细',
   PRICE                VARCHAR(100) COMMENT '参考价格',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建人姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改人姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_CNDRUG COMMENT '中国药典表';

/*==============================================================*/
/* Table: DUG_DRUG                                              */
/*==============================================================*/
CREATE TABLE DUG_DRUG
(
   ID                   INT NOT NULL COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   NAME                 VARCHAR(500) COMMENT '药品名称',
   DESCRIPTION          VARCHAR(4000) COMMENT '描述',
   CREATETIME           DATETIME COMMENT '创建日期',
   UPDATETIME           DATETIME COMMENT '更新日期',
   DIRECTPARENT         VARCHAR(100) COMMENT 'DirectParent',
   SUPERCLASS           VARCHAR(100) COMMENT 'SuperClass',
   INDICATION           VARCHAR(3000) COMMENT 'Indication',
   PHARMACODYNAMICS     VARCHAR(4000) COMMENT 'Pharmacodynamics',
   MECHANISMOFACTION    VARCHAR(4000) COMMENT 'MechanismOfAction',
   TOXICITY             TEXT COMMENT 'Toxicity',
   METABOLISM           VARCHAR(2000) COMMENT 'Metabolism',
   ABSORPTION           VARCHAR(2000) COMMENT 'Absorption',
   PROTEINBINDING       VARCHAR(2000) COMMENT 'ProteinBinding',
   PRIMARY KEY (DRUGBANKID)
);

ALTER TABLE DUG_DRUG COMMENT '药典表';

/*==============================================================*/
/* Table: DUG_DRUG3                                             */
/*==============================================================*/
CREATE TABLE DUG_DRUG3
(
   ID                   INT NOT NULL COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   NAME                 VARCHAR(500) COMMENT '药品名称',
   DESCRIPTION          VARCHAR(4000) COMMENT '描述',
   CREATETIME           DATETIME COMMENT '创建日期',
   UPDATETIME           DATETIME COMMENT '更新日期',
   DIRECTPARENT         VARCHAR(100) COMMENT 'DirectParent',
   SUPERCLASS           VARCHAR(100) COMMENT 'SuperClass',
   INDICATION           VARCHAR(3000) COMMENT 'Indication',
   PHARMACODYNAMICS     VARCHAR(4000) COMMENT 'Pharmacodynamics',
   MECHANISMOFACTION    VARCHAR(4000) COMMENT 'MechanismOfAction',
   TOXICITY             TEXT COMMENT 'Toxicity',
   METABOLISM           VARCHAR(2000) COMMENT 'Metabolism',
   ABSORPTION           VARCHAR(2000) COMMENT 'Absorption',
   PROTEINBINDING       VARCHAR(2000) COMMENT 'ProteinBinding',
   PRIMARY KEY (DRUGBANKID)
);

ALTER TABLE DUG_DRUG3 COMMENT '药典表3';

/*==============================================================*/
/* Table: DUG_DRUGINTERACTIONS                                  */
/*==============================================================*/
CREATE TABLE DUG_DRUGINTERACTIONS
(
   ID                   INT NOT NULL COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   LINKDRUGBANKID       VARCHAR(50) COMMENT '关联的药品',
   NAME                 VARCHAR(200) COMMENT '药品名称',
   DESCRIPTION          VARCHAR(1000) COMMENT '相互作用描述'
);

ALTER TABLE DUG_DRUGINTERACTIONS COMMENT '药品相互作用表';

/*==============================================================*/
/* Table: DUG_DRUGSYNONYMS                                      */
/*==============================================================*/
CREATE TABLE DUG_DRUGSYNONYMS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   LANGUAGE             VARCHAR(500) COMMENT 'Language',
   IDENTIFIER           VARCHAR(500) COMMENT 'Identifier',
   CODER                VARCHAR(500) COMMENT 'Coder',
   SYNONYM              VARCHAR(1000) COMMENT 'Synonym',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_DRUGSYNONYMS COMMENT '药品别名表';

/*==============================================================*/
/* Table: DUG_ENZYMEPO                                          */
/*==============================================================*/
CREATE TABLE DUG_ENZYMEPO
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   ENZYMEID             VARCHAR(100) NOT NULL COMMENT 'EnzymeId',
   POLYPEPTIDEID        VARCHAR(50) NOT NULL COMMENT 'PolypeptideId',
   GENENAME             VARCHAR(50) COMMENT '基因名称',
   AMINOACIDSEQUENCE    TEXT COMMENT '氨基酸序列',
   GENESEQUENCE         TEXT COMMENT '基因序列',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_ENZYMEPO COMMENT '酶多肽表';

/*==============================================================*/
/* Table: DUG_ENZYMES                                           */
/*==============================================================*/
CREATE TABLE DUG_ENZYMES
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   ENZYMESID            VARCHAR(100) NOT NULL COMMENT 'EnzymesId',
   NAME                 VARCHAR(200) COMMENT '生化 酶名称',
   ORGANISM             VARCHAR(1000) COMMENT '有机体',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_ENZYMES COMMENT '生化酶药品管理表';

/*==============================================================*/
/* Table: DUG_ENZYMESACTIONS                                    */
/*==============================================================*/
CREATE TABLE DUG_ENZYMESACTIONS
(
   ID                   INT NOT NULL COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   ENZYMESID            VARCHAR(100) COMMENT 'EnzymesId',
   ACTIONS              VARCHAR(200) COMMENT '作用描述'
);

ALTER TABLE DUG_ENZYMESACTIONS COMMENT '酶作用表';

/*==============================================================*/
/* Table: DUG_ENZYMESIDENTIFIER                                 */
/*==============================================================*/
CREATE TABLE DUG_ENZYMESIDENTIFIER
(
   ID                   INT NOT NULL COMMENT 'Id',
   ENZYMEPOID           INT NOT NULL COMMENT 'EnzymePoId',
   POLYPEPTIDEID        VARCHAR(50) COMMENT 'PolypeptideId',
   RESOURCE             VARCHAR(200) COMMENT 'Resource',
   IDENTIFIER           VARCHAR(200) COMMENT 'Identifier'
);

ALTER TABLE DUG_ENZYMESIDENTIFIER COMMENT '酶标识表';

/*==============================================================*/
/* Table: DUG_ENZYMESSYNONYMS                                   */
/*==============================================================*/
CREATE TABLE DUG_ENZYMESSYNONYMS
(
   ID                   INT NOT NULL COMMENT 'Id',
   ENZYMEPOID           INT NOT NULL COMMENT 'EnzymePoId',
   POLYPEPTIDEID        VARCHAR(50) COMMENT 'PolypeptideId',
   SYNONYMS             VARCHAR(200) COMMENT '酶名称'
);

ALTER TABLE DUG_ENZYMESSYNONYMS COMMENT '同义酶表';

/*==============================================================*/
/* Table: DUG_EXTERNALIDENTIFIERS                               */
/*==============================================================*/
CREATE TABLE DUG_EXTERNALIDENTIFIERS
(
   ID                   INT NOT NULL COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   RESOURCE             VARCHAR(500) COMMENT 'Resource',
   IDENTIFIER           VARCHAR(500) COMMENT 'Identifier'
);

ALTER TABLE DUG_EXTERNALIDENTIFIERS COMMENT '药品外部识别码表';

/*==============================================================*/
/* Table: DUG_PRODUCTS                                          */
/*==============================================================*/
CREATE TABLE DUG_PRODUCTS
(
   ID                   INT NOT NULL COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   NDCID                VARCHAR(200) COMMENT '药品ID',
   NAME                 VARCHAR(200) COMMENT '药品名称',
   STRENGTH             VARCHAR(500) COMMENT '规格',
   STARTEDMARKETINGON   DATETIME COMMENT '上市时间'
);

ALTER TABLE DUG_PRODUCTS COMMENT '药品产品信息表';

/*==============================================================*/
/* Table: DUG_TARGETACTIONS                                     */
/*==============================================================*/
CREATE TABLE DUG_TARGETACTIONS
(
   ID                   INT NOT NULL COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   TARGETID             VARCHAR(50) COMMENT 'TargetId',
   ACTION               VARCHAR(200) COMMENT '作用描述'
);

ALTER TABLE DUG_TARGETACTIONS COMMENT '靶点作用表';

/*==============================================================*/
/* Table: DUG_TARGETIDENTIFIER                                  */
/*==============================================================*/
CREATE TABLE DUG_TARGETIDENTIFIER
(
   ID                   INT NOT NULL COMMENT 'Id',
   TARGETPOID           INT COMMENT 'TargetPoId',
   POLYPEPTIDEID        VARCHAR(50) NOT NULL COMMENT 'PolypeptideId',
   RESOURCE             VARCHAR(200) COMMENT 'Resource',
   IDENTIFIER           VARCHAR(200) COMMENT 'Identifier'
);

ALTER TABLE DUG_TARGETIDENTIFIER COMMENT '靶点标识表';

/*==============================================================*/
/* Table: DUG_TARGETS                                           */
/*==============================================================*/
CREATE TABLE DUG_TARGETS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) NOT NULL COMMENT 'DrugBankId',
   TARGETID             VARCHAR(50) NOT NULL COMMENT 'TargetId',
   NAME                 VARCHAR(500) COMMENT '靶点名称',
   ORGANISM             VARCHAR(500) COMMENT '有机体',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_TARGETS COMMENT '靶点表';

/*==============================================================*/
/* Table: DUG_TARGETSPO                                         */
/*==============================================================*/
CREATE TABLE DUG_TARGETSPO
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   TARGETID             VARCHAR(50) NOT NULL COMMENT 'TargetId',
   POLYPEPTIDEID        VARCHAR(50) NOT NULL COMMENT 'PolypeptideId',
   GENENAME             VARCHAR(50) COMMENT '基因名称',
   AMINOACIDSEQUENCE    TEXT COMMENT '氨基酸序列',
   GENESEQUENCE         TEXT COMMENT '基因序列',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_TARGETSPO COMMENT '靶点多肽表';

/*==============================================================*/
/* Table: DUG_TRANSPORTERACTIONS                                */
/*==============================================================*/
CREATE TABLE DUG_TRANSPORTERACTIONS
(
   ID                   INT NOT NULL COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   TRANSPORTERSID       VARCHAR(100) COMMENT 'TransportersId',
   ACTION               VARCHAR(200) COMMENT '作用描述'
);

ALTER TABLE DUG_TRANSPORTERACTIONS COMMENT '转运蛋白作用表';

/*==============================================================*/
/* Table: DUG_TRANSPORTERIDENTIFIER                             */
/*==============================================================*/
CREATE TABLE DUG_TRANSPORTERIDENTIFIER
(
   ID                   INT NOT NULL COMMENT 'Id',
   TRANSPORTERPOID      INT COMMENT 'TransporterPoId',
   POLYPEPTIDEID        VARCHAR(50) COMMENT 'PolypeptideId',
   RESOURCE             VARCHAR(500) COMMENT 'Resource',
   IDENTIFIER           VARCHAR(500) COMMENT 'Identifier'
);

ALTER TABLE DUG_TRANSPORTERIDENTIFIER COMMENT '转运蛋白标识表';

/*==============================================================*/
/* Table: DUG_TRANSPORTERS                                      */
/*==============================================================*/
CREATE TABLE DUG_TRANSPORTERS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   DRUGBANKID           VARCHAR(50) COMMENT 'DrugBankId',
   TRANSPORTERSID       VARCHAR(100) COMMENT 'TransportersId',
   NAME                 VARCHAR(200) COMMENT '转运蛋白名称',
   ORGANISM             VARCHAR(1000) COMMENT '有机体',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_TRANSPORTERS COMMENT '转运蛋白表';

/*==============================================================*/
/* Table: DUG_TRANSPORTERSPO                                    */
/*==============================================================*/
CREATE TABLE DUG_TRANSPORTERSPO
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'Id',
   ITEMID               INT NOT NULL COMMENT 'ItemId',
   TRANSPORTERSID       VARCHAR(100) COMMENT 'TransportersId',
   POLYPEPTIDEID        VARCHAR(50) COMMENT 'PolypeptideId',
   GENENAME             VARCHAR(50) COMMENT '基因名称',
   AMINOACIDSEQUENCE    TEXT COMMENT '氨基酸序列',
   GENESEQUENCE         TEXT COMMENT '基因序列',
   PRIMARY KEY (ID)
);

ALTER TABLE DUG_TRANSPORTERSPO COMMENT '转运蛋白多肽表';

/*==============================================================*/
/* Table: GN_DRUGALLELE                                         */
/*==============================================================*/
CREATE TABLE GN_DRUGALLELE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   GENEALLELEID         VARCHAR(50) COMMENT '等位基因ID',
   DRUGBANKID           VARCHAR(50) COMMENT '药典ID',
   EFFECTTYPE           NUMERIC(8,0) NOT NULL COMMENT '影响类型',
   EFFECT               VARCHAR(2000) COMMENT '影响',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) NOT NULL COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE GN_DRUGALLELE COMMENT '基因对药物的影响';

/*==============================================================*/
/* Table: GN_GENE                                               */
/*==============================================================*/
CREATE TABLE GN_GENE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   GENENAME             VARCHAR(50) COMMENT '基因名',
   SYNONYMNAME          VARCHAR(50) COMMENT '同义词',
   REFSEQUENCE          TEXT COMMENT '引用序列',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE GN_GENE COMMENT '基因';

/*==============================================================*/
/* Table: GN_GENEALLELE                                         */
/*==============================================================*/
CREATE TABLE GN_GENEALLELE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   GENEALLELENAME       VARCHAR(50) COMMENT '等位基因名',
   GENENAME             VARCHAR(50) COMMENT '基因名',
   SYNONYMNAME          VARCHAR(50) COMMENT '同义词',
   STANDNAME            VARCHAR(50) COMMENT '标准名',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE GN_GENEALLELE COMMENT '等位基因';

/*==============================================================*/
/* Table: GN_GENEALLELELOCUS                                    */
/*==============================================================*/
CREATE TABLE GN_GENEALLELELOCUS
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   GENEALLELENAME       VARCHAR(50) COMMENT '等位基因名',
   GENENAME             VARCHAR(50) COMMENT '基因名',
   LOCUSTYPE            VARCHAR(10) COMMENT '类型',
   STARTPOSITION        NUMERIC(8,0) COMMENT '起始位点',
   ENDPOSITION          NUMERIC(8,0) COMMENT '终止位点',
   STANDARDVALUE        VARCHAR(50) COMMENT '标准值',
   VARIANTVALUE         VARCHAR(50) COMMENT '变化值',
   VARIANTTYPE          VARCHAR(50) COMMENT '变化方式',
   VARIANTCATEGORY      VARCHAR(50) COMMENT '变化种类',
   VARIANTCODE          VARCHAR(200) COMMENT '变化编码',
   CREATEUSERID         VARCHAR(50) COMMENT '创建者ID',
   CREATEUSERNAME       VARCHAR(50) COMMENT '创建者姓名',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改者ID',
   EDITUSERNAME         VARCHAR(50) COMMENT '修改者姓名',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   OWNERNAME            VARCHAR(50) COMMENT '所属人姓名',
   ISDELETED            NUMERIC(1,0) COMMENT '是否删除',
   PRIMARY KEY (ID)
);

ALTER TABLE GN_GENEALLELELOCUS COMMENT '位点信息';

/*==============================================================*/
/* Table: HPN_BODYTYPE                                          */
/*==============================================================*/
CREATE TABLE HPN_BODYTYPE
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   BODYDESC             VARCHAR(4000) COMMENT 'BodyDesc',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_BODYTYPE COMMENT 'HPN_BodyType';

/*==============================================================*/
/* Table: HPN_MIGRATIONHISTORY                                  */
/*==============================================================*/
CREATE TABLE HPN_MIGRATIONHISTORY
(
   MIGRATIONID          VARCHAR(150) NOT NULL COMMENT 'MigrationId',
   CONTEXTKEY           VARCHAR(300) COMMENT 'ContextKey',
   MODEL                VARCHAR(4000) NOT NULL COMMENT 'Model',
   PRODUCTVERSION       VARCHAR(50) NOT NULL COMMENT 'ProductVersion',
   PRIMARY KEY (MIGRATIONID)
);

ALTER TABLE HPN_MIGRATIONHISTORY COMMENT 'HPN_MigrationHistory';

/*==============================================================*/
/* Table: HPN_RESULTDESC                                        */
/*==============================================================*/
CREATE TABLE HPN_RESULTDESC
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   TEMPLATENAME         VARCHAR(4000) COMMENT 'TemplateName',
   SUBRESULTNAME        VARCHAR(4000) COMMENT 'SubResultName',
   MINSCORES            FLOAT NOT NULL COMMENT 'MinScores',
   MAXSCORES            FLOAT NOT NULL COMMENT 'MaxScores',
   RESULTNAME           VARCHAR(4000) COMMENT 'ResultName',
   DESCRIPTION          VARCHAR(4000) COMMENT 'Description',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_RESULTDESC COMMENT 'HPN_ResultDesc';

/*==============================================================*/
/* Table: HPN_SENDVERCODELOG                                    */
/*==============================================================*/
CREATE TABLE HPN_SENDVERCODELOG
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   INPUTTIME            DATETIME NOT NULL COMMENT 'InputTime',
   MOBILEPHONE          VARCHAR(4000) COMMENT 'MobilePhone',
   FORMURL              VARCHAR(4000) COMMENT 'FormUrl',
   REMARK               VARCHAR(4000) COMMENT 'Remark',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_SENDVERCODELOG COMMENT 'HPN_SendVercodeLog';

/*==============================================================*/
/* Table: HPN_STANDARDADVISE                                    */
/*==============================================================*/
CREATE TABLE HPN_STANDARDADVISE
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   TEMPLATERESULTID     INT NOT NULL COMMENT 'TemplateResultID',
   BODYTYPE             INT NOT NULL COMMENT 'BodyType',
   CONTENT              VARCHAR(4000) COMMENT 'Content',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_STANDARDADVISE COMMENT 'HPN_StandardAdvise';

/*==============================================================*/
/* Table: HPN_SUBRESULTDESC                                     */
/*==============================================================*/
CREATE TABLE HPN_SUBRESULTDESC
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   SUBRESULTNAME        VARCHAR(4000) COMMENT 'SubResultName',
   TEMPLATENAME         VARCHAR(4000) COMMENT 'TemplateName',
   QUESTIONNAME         VARCHAR(4000) COMMENT 'QuestionName',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_SUBRESULTDESC COMMENT 'HPN_SubResultDesc';

/*==============================================================*/
/* Table: HPN_TEMPLATE                                          */
/*==============================================================*/
CREATE TABLE HPN_TEMPLATE
(
   ID                   INT NOT NULL COMMENT 'ID',
   TITLE                VARCHAR(200) COMMENT 'Title',
   TEMPLATEQUARRY       VARCHAR(200) COMMENT 'TemplateQuarry',
   HPNLABEL             VARCHAR(200) COMMENT 'HPNLabel',
   ACTIONNAME           VARCHAR(200) COMMENT 'ActionName',
   ICON                 VARCHAR(200) COMMENT 'Icon',
   HPNINDEX             INT NOT NULL COMMENT 'HPNIndex',
   ENABLED              INT NOT NULL COMMENT 'Enabled',
   HPNTYPE              INT NOT NULL COMMENT 'HPNType',
   REMARK               VARCHAR(200) COMMENT 'Remark',
   CREATETIME           DATETIME NOT NULL COMMENT 'CreateTime',
   CREATEUSER           VARCHAR(200) COMMENT 'CreateUser',
   LASTMODIFYTIME       DATETIME NOT NULL COMMENT 'LastModifyTime',
   LASTMODIFYUSER       VARCHAR(200) COMMENT 'LastModifyUser',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TEMPLATE COMMENT 'HPN_Template';

/*==============================================================*/
/* Table: HPN_TEMPLATEITEM                                      */
/*==============================================================*/
CREATE TABLE HPN_TEMPLATEITEM
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   TEMPLATENAME         VARCHAR(200) COMMENT 'TemplateName',
   HPNINDEX             INT NOT NULL COMMENT 'HPNIndex',
   TAGTYPE              INT NOT NULL COMMENT 'TagType',
   HPNTITLE             VARCHAR(200) COMMENT 'HPNTitle',
   HPNNAME              VARCHAR(200) COMMENT 'HPNName',
   HTML                 VARCHAR(200) COMMENT 'Html',
   REMARK               VARCHAR(200) COMMENT 'Remark',
   NEXTITEM             VARCHAR(200) COMMENT 'NextItem',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TEMPLATEITEM COMMENT 'HPN_TemplateItem';

/*==============================================================*/
/* Table: HPN_TEMPLATEITEMOPTIONS                               */
/*==============================================================*/
CREATE TABLE HPN_TEMPLATEITEMOPTIONS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   TEMPLATENAME         VARCHAR(200) COMMENT 'TemplateName',
   TEMPLATEITEMNAME     VARCHAR(200) COMMENT 'TemplateItemName',
   TEXT                 VARCHAR(200) COMMENT 'Text',
   HPNVALUE             VARCHAR(200) COMMENT 'HPNValue',
   HPNNAME              VARCHAR(200) COMMENT 'HPNName',
   IDATTR               VARCHAR(200) COMMENT 'IdAttr',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TEMPLATEITEMOPTIONS COMMENT 'HPN_TemplateItemOptions';

/*==============================================================*/
/* Table: HPN_TEMPLATEITEMRESULT                                */
/*==============================================================*/
CREATE TABLE HPN_TEMPLATEITEMRESULT
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   ITEMID               INT NOT NULL COMMENT 'ItemID',
   HPNVALUE             VARCHAR(200) COMMENT 'HPNValue',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TEMPLATEITEMRESULT COMMENT 'HPN_TemplateItemResult';

/*==============================================================*/
/* Table: HPN_TEMPLATETYPE                                      */
/*==============================================================*/
CREATE TABLE HPN_TEMPLATETYPE
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   DESCRIPTION          VARCHAR(4000) COMMENT 'Description',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TEMPLATETYPE COMMENT 'HPN_TemplateType';

/*==============================================================*/
/* Table: HPN_TESTRESULT                                        */
/*==============================================================*/
CREATE TABLE HPN_TESTRESULT
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   INPUTTIME            DATETIME NOT NULL COMMENT 'InputTime',
   TESTNO               VARCHAR(200) COMMENT 'TestNo',
   OTHERTESTNO          VARCHAR(200) COMMENT 'OtherTestNo',
   USERID               INT NOT NULL COMMENT 'UserID',
   TEMPLATENAME         VARCHAR(200) COMMENT 'TemplateName',
   STATE                INT NOT NULL COMMENT 'State',
   IP                   VARCHAR(200) COMMENT 'IP',
   RESULT               VARCHAR(200) COMMENT 'Result',
   REMARK               VARCHAR(200) COMMENT 'Remark',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TESTRESULT COMMENT 'HPN_TestResult';

/*==============================================================*/
/* Table: HPN_TESTRESULTDETAILS                                 */
/*==============================================================*/
CREATE TABLE HPN_TESTRESULTDETAILS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   INPUTTIME            DATETIME NOT NULL COMMENT 'InputTime',
   TESTNO               VARCHAR(4000) COMMENT 'TestNo',
   TEMPLATEITEMNAME     VARCHAR(4000) COMMENT 'TemplateItemName',
   ITEMRESULTID         VARCHAR(4000) COMMENT 'ItemResultID',
   ITEMRESULT           VARCHAR(4000) COMMENT 'ItemResult',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_TESTRESULTDETAILS COMMENT 'HPN_TestResultDetails';

/*==============================================================*/
/* Table: HPN_USERTYPE                                          */
/*==============================================================*/
CREATE TABLE HPN_USERTYPE
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   HPNNAME              VARCHAR(4000) COMMENT 'HPNName',
   REMARK               VARCHAR(4000) COMMENT 'Remark',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_USERTYPE COMMENT 'HPN_UserType';

/*==============================================================*/
/* Table: HPN_USERS                                             */
/*==============================================================*/
CREATE TABLE HPN_USERS
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   MOBILEPHONE          VARCHAR(200) COMMENT 'MobilePhone',
   EMAIL                VARCHAR(200) COMMENT 'Email',
   LOGINNAME            VARCHAR(200) COMMENT 'LoginName',
   LOGINPWD             VARCHAR(200) COMMENT 'LoginPwd',
   HPNNAME              VARCHAR(200) COMMENT 'HPNName',
   IDCARD               VARCHAR(200) COMMENT 'IDCard',
   HPNTYPE              INT NOT NULL COMMENT 'HPNType',
   VIPLEVEL             INT NOT NULL COMMENT 'VipLevel',
   HEIGHT               FLOAT NOT NULL COMMENT 'Height',
   WEIGHT               FLOAT NOT NULL COMMENT 'Weight',
   ISDELETE             INT NOT NULL COMMENT 'IsDelete',
   LASTMODIFYTIME       DATETIME NOT NULL COMMENT 'LastModifyTime',
   LASTMODIFYUSER       VARCHAR(200) COMMENT 'LastModifyUser',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_USERS COMMENT 'HPN_Users';

/*==============================================================*/
/* Table: HPN_VIPDESC                                           */
/*==============================================================*/
CREATE TABLE HPN_VIPDESC
(
   ID                   INT NOT NULL AUTO_INCREMENT COMMENT 'ID',
   HPNDESC              VARCHAR(2000) COMMENT 'HPNDesc',
   PRIMARY KEY (ID)
);

ALTER TABLE HPN_VIPDESC COMMENT 'HPN_VipDesc';

/*==============================================================*/
/* Table: HR_ACTIVITYLOG                                        */
/*==============================================================*/
CREATE TABLE HR_ACTIVITYLOG
(
   ID                   VARCHAR(50) NOT NULL COMMENT '字典ID',
   ACTIVITYLOGTYPENAME  VARCHAR(100) COMMENT '父字典ID',
   CUSTOMERID           VARCHAR(50) COMMENT '字典类型',
   ACCOMMENT            VARCHAR(200) COMMENT '字典名称',
   CREATEDATE           DATETIME COMMENT '创建时间',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_ACTIVITYLOG COMMENT '操作日志表';

/*==============================================================*/
/* Table: HR_ANNUALINCOME                                       */
/*==============================================================*/
CREATE TABLE HR_ANNUALINCOME
(
   ANNUALINCOMEID       VARCHAR(50) NOT NULL COMMENT '用户账号',
   PERSONID             VARCHAR(50) COMMENT '平台用户id',
   ANNUALINCOME         VARCHAR(50) COMMENT '年收入',
   CREATTIME            DATETIME COMMENT '创建时间',
   CREATBY              VARCHAR(50) COMMENT '创建者',
   MODIFYTIME           DATETIME COMMENT '修改时间',
   MODIFYBY             VARCHAR(50) COMMENT '修改者',
   PRIMARY KEY (ANNUALINCOMEID)
);

ALTER TABLE HR_ANNUALINCOME COMMENT '财务信息';

/*==============================================================*/
/* Table: HR_AREA                                               */
/*==============================================================*/
CREATE TABLE HR_AREA
(
   AREAID               NUMERIC(9,0) NOT NULL COMMENT 'AreaId',
   AREANAME             VARCHAR(50) NOT NULL COMMENT 'AreaName',
   PARENTID             NUMERIC(9,0) NOT NULL COMMENT 'ParentId',
   ISDELETED            NUMERIC(3,0) COMMENT 'IsDeleted',
   PRIMARY KEY (AREAID)
);

ALTER TABLE HR_AREA COMMENT 'HR_Area';

/*==============================================================*/
/* Table: HR_ATTACHMENT                                         */
/*==============================================================*/
CREATE TABLE HR_ATTACHMENT
(
   ATTACHMENTRECORDID   VARCHAR(50) NOT NULL COMMENT 'GUID',
   RECORDID             VARCHAR(50) COMMENT '表单记录ID',
   ATTACHMENTID         VARCHAR(50) COMMENT '附件ID',
   ATTACHMENTURL        VARCHAR(2000) COMMENT '附件地址URL',
   ATTACHMENTPATH       VARCHAR(2000) COMMENT '附件绝对路径',
   CREATEUSER           VARCHAR(50) COMMENT '创建人',
   CREATETIME           DATETIME COMMENT '创建时间',
   EDITUSER             VARCHAR(50) COMMENT '修改人',
   EDITDATE             DATETIME COMMENT '修改时间',
   PRIMARY KEY (ATTACHMENTRECORDID)
);

ALTER TABLE HR_ATTACHMENT COMMENT '附件表';

/*==============================================================*/
/* Table: HR_CNR_USER                                           */
/*==============================================================*/
CREATE TABLE HR_CNR_USER
(
   USERID               VARCHAR(50) NOT NULL COMMENT '用户账号',
   PERSONID             VARCHAR(50) COMMENT '平台用户id',
   LOCALISM             VARCHAR(50) COMMENT '方言',
   AREA                 VARCHAR(50) COMMENT '地域',
   DISEASE              VARCHAR(2000) COMMENT '就诊疾病',
   CREATTIME            DATETIME COMMENT '创建时间',
   CREATBY              VARCHAR(50) COMMENT '创建者',
   MODIFYTIME           DATETIME COMMENT '修改时间',
   MODIFYBY             VARCHAR(50) COMMENT '修改者',
   IDCARD               VARCHAR(50) COMMENT '身份证号',
   PRIMARY KEY (USERID)
);

ALTER TABLE HR_CNR_USER COMMENT 'Cancer用户表(基础资料)';

/*==============================================================*/
/* Table: HR_DICTIONARY                                         */
/*==============================================================*/
CREATE TABLE HR_DICTIONARY
(
   DICTIONARYID         VARCHAR(50) NOT NULL COMMENT '字典ID',
   FATHERID             VARCHAR(50) COMMENT '父字典ID',
   DICTIONCATEGORY      VARCHAR(50) COMMENT '字典类型',
   DICTIONCATEGORYNAME  VARCHAR(50) COMMENT '字典类型名',
   DICTIONARYNAME       VARCHAR(50) COMMENT '字典名称',
   DICTIONARYVALUE      NUMERIC(8,0) COMMENT '字典值',
   CREATEUSER           VARCHAR(50) COMMENT '创建人',
   CREATEDATE           DATETIME COMMENT '创建时间',
   UPDATEUSER           VARCHAR(50) COMMENT '修改人',
   UPDATEDATE           DATETIME COMMENT '修改时间',
   REMARK               VARCHAR(2000) COMMENT '备注',
   SYSTEMNAME           VARCHAR(100) COMMENT '系统类型名:公共Common,人力资源管理:HR,办公自动化OA,物流LM',
   SYSTEMCODE           VARCHAR(100) COMMENT '系统类型编码',
   ORDERNUMBER          NUMERIC(8,0) COMMENT '排序号',
   SYSTEMNEED           VARCHAR(1) COMMENT '系统必须字典:用户不能修改',
   PRIMARY KEY (DICTIONARYID)
);

ALTER TABLE HR_DICTIONARY COMMENT '系统数据字典';

/*==============================================================*/
/* Table: HR_DISEASEHISTORY                                     */
/*==============================================================*/
CREATE TABLE HR_DISEASEHISTORY
(
   DISEASEHISTORYID     VARCHAR(50) NOT NULL COMMENT '用户账号',
   PERSONID             VARCHAR(50) COMMENT '用户id',
   DISEASENAME          VARCHAR(2000) COMMENT '疾病名称',
   ANNUALINCOME         VARCHAR(50) COMMENT 'ICD10名称',
   TREATPROCESS         VARCHAR(2000) COMMENT '治疗过程',
   CREATTIME            DATETIME COMMENT '就诊时间',
   HOSPITAL             VARCHAR(500) COMMENT '就诊医院',
   CREATUSER            VARCHAR(50) COMMENT '创建者',
   EDITUSER             VARCHAR(50) COMMENT '修改者',
   EDITTIME             DATETIME COMMENT '修改时间',
   STARTTIME            DATETIME COMMENT '发病时间',
   RECOVERTIME          DATETIME COMMENT '康复时间',
   CONFIRMEDTIME        DATETIME COMMENT '诊断时间',
   CONFIRMEDAGE         VARCHAR(50) COMMENT '诊断年龄',
   CONFIRMEDADDRESS     VARCHAR(200) COMMENT '诊断地点',
   CONFIRMEDMODE        VARCHAR(200) COMMENT '诊断方式',
   CONFIRMEDRELUST      VARCHAR(2000) COMMENT '诊断结果',
   SYMPTOMDESCRIPTION   VARCHAR(2000) COMMENT '症状描述',
   ISCANCER             VARCHAR(50) COMMENT '是否肿瘤',
   CANCERCODE           VARCHAR(50) COMMENT '肿瘤疾病编码',
   CANCERNAME           VARCHAR(200) COMMENT '肿瘤名称',
   CANCERPOSITION       VARCHAR(200) COMMENT '肿瘤位置',
   CELLULATYPE          VARCHAR(200) COMMENT '病理细胞类型',
   CLINICALSTAGES       VARCHAR(50) COMMENT '临床分期',
   TUMOR                VARCHAR(200) COMMENT 'Tumor(肿瘤本身)',
   N                    VARCHAR(200) COMMENT 'N(淋巴结转移情况)',
   M                    VARCHAR(200) COMMENT 'M(远程转移)',
   GENE                 VARCHAR(200) COMMENT 'Gene组型',
   ISREAPPEAR           VARCHAR(200) COMMENT '是否为复发',
   PRIMARY KEY (DISEASEHISTORYID)
);

ALTER TABLE HR_DISEASEHISTORY COMMENT '既往病史';

/*==============================================================*/
/* Table: HR_EXAMINEITEMS                                       */
/*==============================================================*/
CREATE TABLE HR_EXAMINEITEMS
(
   ITEMID               NUMERIC(9,0) NOT NULL COMMENT 'ITEMID',
   ITEMNO               VARCHAR(20) COMMENT 'ITEMNO',
   ITEMNAME             VARCHAR(100) COMMENT 'ITEMNAME',
   DESCRIPTION          VARCHAR(500) COMMENT 'DESCRIPTION',
   VALUETYPE            VARCHAR(3) COMMENT 'VALUETYPE',
   CODEVALUE            VARCHAR(50) COMMENT 'CODEVALUE',
   CREATEDATE           DATETIME COMMENT 'CREATEDATE',
   CREATEBY             VARCHAR(100) COMMENT 'CREATEBY',
   UPDATEBY             VARCHAR(100) COMMENT 'UPDATEBY',
   UPDATEDATE           DATETIME COMMENT 'UPDATEDATE',
   STATUS               NUMERIC(3,0) COMMENT 'STATUS',
   PRIMARY KEY (ITEMID)
);

ALTER TABLE HR_EXAMINEITEMS COMMENT 'HR_EXAMINEITEMS';

/*==============================================================*/
/* Table: HR_EXAMINERECORD                                      */
/*==============================================================*/
CREATE TABLE HR_EXAMINERECORD
(
   EXAMID               NUMERIC(9,0) NOT NULL COMMENT 'EXAMID',
   EXAMNO               VARCHAR(2000) COMMENT 'EXAMNO',
   EXAMTYPE             VARCHAR(3) NOT NULL COMMENT 'EXAMTYPE',
   VISITWAY             VARCHAR(3) COMMENT 'VISITWAY',
   DOCTORNAME           VARCHAR(50) COMMENT 'DOCTORNAME',
   CREATEDATE           DATETIME COMMENT 'CREATEDATE',
   CREATEBY             VARCHAR(100) COMMENT 'CREATEBY',
   UPDATEDATE           DATETIME COMMENT 'UPDATEDATE',
   UPDATEBY             VARCHAR(100) COMMENT 'UPDATEBY',
   STATUS               NUMERIC(3,0) NOT NULL DEFAULT 1 COMMENT 'STATUS',
   EXAMDATE             DATETIME COMMENT 'EXAMDATE',
   PERSONID             NUMERIC(9,0) COMMENT 'PERSONID',
   PRIMARY KEY (EXAMID)
);

ALTER TABLE HR_EXAMINERECORD COMMENT 'HR_EXAMINERECORD';

/*==============================================================*/
/* Table: HR_EXAMINERESULT                                      */
/*==============================================================*/
CREATE TABLE HR_EXAMINERESULT
(
   RESULTID             NUMERIC(9,0) NOT NULL COMMENT 'RESULTID',
   ITEMCODE             VARCHAR(2000) COMMENT 'ITEMCODE',
   RESULT               VARCHAR(2000) COMMENT 'RESULT',
   CREATEDATE           DATETIME COMMENT 'CREATEDATE',
   CREATEBY             VARCHAR(100) COMMENT 'CREATEBY',
   UPDATEDATE           DATETIME COMMENT 'UPDATEDATE',
   UPDATEBY             VARCHAR(100) COMMENT 'UPDATEBY',
   STATUS               NUMERIC(3,0) NOT NULL DEFAULT 1 COMMENT 'STATUS',
   EXAMID               NUMERIC(9,0) COMMENT 'EXAMID',
   PRIMARY KEY (RESULTID)
);

ALTER TABLE HR_EXAMINERESULT COMMENT 'HR_EXAMINERESULT';

/*==============================================================*/
/* Table: HR_EXAMINETEMPLATE                                    */
/*==============================================================*/
CREATE TABLE HR_EXAMINETEMPLATE
(
   TEMPLATEID           VARCHAR(50) NOT NULL COMMENT '模板ID',
   TEMPLATENAME         VARCHAR(50) COMMENT '模板名称',
   DESCRIPTION          VARCHAR(500) COMMENT '描述信息',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            CHAR(1) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (TEMPLATEID)
);

ALTER TABLE HR_EXAMINETEMPLATE COMMENT '检验模板';

/*==============================================================*/
/* Table: HR_EXAMINETEMPLATES                                   */
/*==============================================================*/
CREATE TABLE HR_EXAMINETEMPLATES
(
   TEMPLATEID           NUMERIC(9,0) NOT NULL COMMENT 'TEMPLATEID',
   TEMPLATENO           VARCHAR(20) COMMENT 'TEMPLATENO',
   TEMPLATENAME         VARCHAR(50) COMMENT 'TEMPLATENAME',
   CREATEDATE           DATETIME COMMENT 'CREATEDATE',
   CREATEBY             VARCHAR(100) COMMENT 'CREATEBY',
   UPDATEDATE           DATETIME COMMENT 'UPDATEDATE',
   UPDATEBY             VARCHAR(100) COMMENT 'UPDATEBY',
   STATUS               NUMERIC(3,0) COMMENT 'STATUS',
   DESCRIPTION          VARCHAR(500) COMMENT 'DESCRIPTION',
   PRIMARY KEY (TEMPLATEID)
);

ALTER TABLE HR_EXAMINETEMPLATES COMMENT 'HR_EXAMINETEMPLATES';

/*==============================================================*/
/* Table: HR_ENTITYRELATION                                     */
/*==============================================================*/
CREATE TABLE HR_ENTITYRELATION
(
   ID                   VARCHAR(100) NOT NULL COMMENT 'entityid',
   SYSTEMCODE           VARCHAR(100) COMMENT '系统名Code',
   ENTITYCODE           VARCHAR(100) COMMENT '实体Code',
   ENTITYID             VARCHAR(100) COMMENT '实体ID',
   REFSYSTEMCODE        VARCHAR(100) COMMENT '关联系统Code',
   REFENTITYCODE        VARCHAR(100) COMMENT '关联实体Code',
   REFENTITYID          VARCHAR(100) COMMENT '关联实体ID',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_ENTITYRELATION COMMENT '系统数据字典';

/*==============================================================*/
/* Table: HR_FD_FIMILYMEMBER                                    */
/*==============================================================*/
CREATE TABLE HR_FD_FIMILYMEMBER
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID(GUID)',
   USERID               VARCHAR(50) COMMENT '用户ID',
   RELATIONSHIP         VARCHAR(50) COMMENT '关系',
   NAME                 VARCHAR(50) COMMENT '姓名',
   SEX                  INT COMMENT '性别(GB)',
   ISTWINS              INT COMMENT '是否双胞胎',
   ISIDENTICAL          INT COMMENT '是否同卵',
   ISALIVE              INT COMMENT '是否在世',
   DEADREASON           VARCHAR(50) COMMENT '去世原因',
   CREATEDTIME          DATETIME COMMENT '创建时间',
   CREATEDBY            VARCHAR(50) COMMENT '创建者',
   MODIFIEDTIME         DATETIME COMMENT '修改时间',
   MODIFIEDBY           VARCHAR(50) COMMENT '修改者',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_FD_FIMILYMEMBER COMMENT '家族成员表';

/*==============================================================*/
/* Table: HR_FD_FIMILYMEMBERDISEASE                             */
/*==============================================================*/
CREATE TABLE HR_FD_FIMILYMEMBERDISEASE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   MEMBERID             VARCHAR(50) NOT NULL COMMENT '家庭成员ID',
   DISEASECODE          VARCHAR(50) COMMENT '疾病编码',
   DISEASENAME          VARCHAR(50) COMMENT '疾病名称',
   ATTACKDATE           DATETIME COMMENT '首次发病时间',
   DIAGNOSISAGE         INT COMMENT '发病年龄',
   ISINFECTIOUS         INT COMMENT '是否传染病',
   TREATMENTHOSPITAL    VARCHAR(2000) COMMENT '诊断医院',
   TREATMENT            VARCHAR(4000) COMMENT '治疗经过',
   TREATMENTRESULT      VARCHAR(2000) COMMENT '治疗效果',
   PRIMARY KEY (ID, MEMBERID)
);

ALTER TABLE HR_FD_FIMILYMEMBERDISEASE COMMENT '家族病史';

/*==============================================================*/
/* Table: HR_FD_FIMILYRELATION                                  */
/*==============================================================*/
CREATE TABLE HR_FD_FIMILYRELATION
(
   ID                   VARCHAR(32) NOT NULL COMMENT 'ID(GUID)',
   CODE                 VARCHAR(20) COMMENT '关系编码',
   TEXT                 VARCHAR(50) COMMENT '关系',
   VALUE                INT COMMENT '关系值(GB4761)',
   SIDE                 VARCHAR(50) COMMENT '系别',
   GENERATE             VARCHAR(50) COMMENT '辈分',
   REQUIRD              INT COMMENT '是否必填',
   APPLY                INT COMMENT '是否可添加',
   SORT                 INT COMMENT '排序',
   SEX                  INT COMMENT '性别',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_FD_FIMILYRELATION COMMENT '家族关系字典';

/*==============================================================*/
/* Table: HR_FAMILY                                             */
/*==============================================================*/
CREATE TABLE HR_FAMILY
(
   FAMILYID             NUMERIC(9,0) NOT NULL COMMENT 'FamilyId',
   HOUSEHOLDERID        NUMERIC(9,0) NOT NULL COMMENT 'HouseholderId',
   HOUSEHOLDERNAME      VARCHAR(50) NOT NULL COMMENT 'HouseholderName',
   CARDNO               VARCHAR(20) COMMENT 'CardNo',
   NATIONALITY          NUMERIC(6,0) COMMENT 'Nationality',
   PROVINCEID           NUMERIC(6,0) COMMENT 'ProvinceId',
   CITYID               NUMERIC(6,0) COMMENT 'CityId',
   COUNTRYID            NUMERIC(6,0) COMMENT 'CountryId',
   TOWNID               NUMERIC(9,0) COMMENT 'TownId',
   ADDRESS              VARCHAR(255) COMMENT 'Address',
   COMMUNITYID          NUMERIC(6,0) COMMENT 'CommunityId',
   ZIPCODE              VARCHAR(255) COMMENT 'ZipCode',
   CREATIONTIME         DATETIME COMMENT 'CreationTime',
   CREATORUSERID        NUMERIC(9,0) COMMENT 'CreatorUserId',
   LASTMODIFICATIONTIME DATETIME COMMENT 'LastModificationTime',
   LASTMODIFIERUSERID   NUMERIC(9,0) COMMENT 'LastModifierUserId',
   DELETERUSERID        NUMERIC(9,0) COMMENT 'DeleterUserId',
   DELETIONTIME         DATETIME COMMENT 'DeletionTime',
   ISDELETED            NUMERIC(3,0) COMMENT 'IsDeleted',
   PRIMARY KEY (FAMILYID)
);

ALTER TABLE HR_FAMILY COMMENT 'HR_Family';

/*==============================================================*/
/* Table: HR_FAMILYMEMBER                                       */
/*==============================================================*/
CREATE TABLE HR_FAMILYMEMBER
(
   FMEMBERID            NUMERIC(9,0) NOT NULL COMMENT 'FMemberId',
   FAMILYID             NUMERIC(9,0) NOT NULL COMMENT 'FamilyId',
   MEMBERNAME           VARCHAR(50) NOT NULL COMMENT 'MemberName',
   CARDNO               VARCHAR(20) COMMENT 'CardNo',
   RELATEID             NUMERIC(4,0) NOT NULL COMMENT 'RelateId',
   CREATIONTIME         DATETIME COMMENT 'CreationTime',
   CREATORUSERID        NUMERIC(9,0) COMMENT 'CreatorUserId',
   LASTMODIFICATIONTIME DATETIME COMMENT 'LastModificationTime',
   LASTMODIFIERUSERID   NUMERIC(9,0) COMMENT 'LastModifierUserId',
   DELETERUSERID        NUMERIC(9,0) COMMENT 'DeleterUserId',
   DELETIONTIME         DATETIME COMMENT 'DeletionTime',
   ISDELETED            NUMERIC(3,0) COMMENT 'IsDeleted',
   PRIMARY KEY (FMEMBERID)
);

ALTER TABLE HR_FAMILYMEMBER COMMENT 'HR_FamilyMember';

/*==============================================================*/
/* Table: HR_FILEUPLOAD                                         */
/*==============================================================*/
CREATE TABLE HR_FILEUPLOAD
(
   FILEUPLOADID         VARCHAR(50) NOT NULL COMMENT '用户账号',
   MODELNAME            VARCHAR(50) COMMENT '模块名称',
   MODELCODE            VARCHAR(200) COMMENT '模块代码',
   FORMID               VARCHAR(50) COMMENT '表单记录id',
   FILEPAH              VARCHAR(2000) COMMENT '文件地址',
   FILENAME             VARCHAR(50) COMMENT '文件名称',
   CREATTIME            DATETIME COMMENT '创建时间',
   CREATBY              VARCHAR(50) COMMENT '创建者',
   MODIFYTIME           DATETIME COMMENT '修改时间',
   MODIFYBY             VARCHAR(50) COMMENT '修改者',
   REMARK               VARCHAR(200) COMMENT '备注',
   PRIMARY KEY (FILEUPLOADID)
);

ALTER TABLE HR_FILEUPLOAD COMMENT '文件上传记录';

/*==============================================================*/
/* Table: HR_ITEMSTANDVAL                                       */
/*==============================================================*/
CREATE TABLE HR_ITEMSTANDVAL
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   ITEMNO               VARCHAR(20) COMMENT '项编号',
   NAMEENG              VARCHAR(50) COMMENT '项英文名称',
   MINVALUE             FLOAT COMMENT '最小值',
   CTMAXVALUE           FLOAT COMMENT '最大值',
   UNIT                 VARCHAR(20) COMMENT '单位',
   CREATEUSERID         VARCHAR(50) COMMENT '创建人',
   CREATEDATETIME       DATETIME COMMENT '创建时间',
   EDITUSERID           VARCHAR(50) COMMENT '修改人',
   EDITDATETIME         DATETIME COMMENT '修改时间',
   ISDELETED            CHAR(1) COMMENT '是否删除',
   OWNERID              VARCHAR(50) COMMENT '所属人ID',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_ITEMSTANDVAL COMMENT '模板项值设置';

/*==============================================================*/
/* Table: HR_IMAGEEXAMINATION                                   */
/*==============================================================*/
CREATE TABLE HR_IMAGEEXAMINATION
(
   IMAGEEXAMID          VARCHAR(50) NOT NULL COMMENT '影像检查ID',
   HISTORYID            VARCHAR(50) NOT NULL COMMENT '就诊史ID',
   CHECKTYPE            VARCHAR(50) COMMENT '报告单类型',
   CHECKTIME            DATETIME COMMENT '检查日期',
   DEPARTMENT           VARCHAR(100) COMMENT '申请科室',
   CHECKPOSITION        VARCHAR(200) COMMENT '检查部位',
   REPORTCONTENT        VARCHAR(2000) COMMENT '检查所见',
   REPORTTIME           DATETIME COMMENT '报告时间',
   REPORTDOCTOR         VARCHAR(100) COMMENT '报告医生',
   AUDITDOCTOR          VARCHAR(100) COMMENT '审核医生',
   IMAGEURL             VARCHAR(2000) COMMENT '影像附件地址',
   PRIMARY KEY (IMAGEEXAMID)
);

ALTER TABLE HR_IMAGEEXAMINATION COMMENT '影像学检查结果';

/*==============================================================*/
/* Table: HR_LABORATORYRESULT                                   */
/*==============================================================*/
CREATE TABLE HR_LABORATORYRESULT
(
   LABRESULTID          VARCHAR(50) NOT NULL COMMENT '实验室结果ID',
   HISTORYID            VARCHAR(50) NOT NULL COMMENT '就诊史ID',
   DIAGNOSISTIME        DATETIME COMMENT '检查时间',
   DIAGNOSIS            VARCHAR(200) COMMENT '住院/门诊号',
   HOSPITAL             VARCHAR(200) COMMENT '科室',
   DIAGNOSISREPORT      VARCHAR(50) COMMENT '床号',
   CI                   VARCHAR(2000) COMMENT '临床印象',
   MEDICALHISATTACHMENT VARCHAR(2000) COMMENT '检验目的',
   LABMODELID           VARCHAR(50) COMMENT '检查模板ID',
   LABTABELNAME         VARCHAR(200) COMMENT '检查表名',
   TESTSTANDARD         VARCHAR(200) COMMENT '检查标准',
   PRIMARY KEY (LABRESULTID)
);

ALTER TABLE HR_LABORATORYRESULT COMMENT '实验室检查结果';

/*==============================================================*/
/* Table: HR_LABORATORYTESTITEM                                 */
/*==============================================================*/
CREATE TABLE HR_LABORATORYTESTITEM
(
   TESTITEMID           VARCHAR(50) NOT NULL COMMENT '项目结果ID',
   ORDERNUMBER          VARCHAR(50) COMMENT '序号',
   ITEMNAME             VARCHAR(200) COMMENT '分析项目名称',
   ITEMNAMEEN           VARCHAR(200) COMMENT '英文名称',
   RESLUT               VARCHAR(200) COMMENT '结果',
   UOM                  VARCHAR(200) COMMENT '单位',
   NORMALVALUE          VARCHAR(200) COMMENT '正常参考值',
   REFERENCEVALUE       VARCHAR(200) COMMENT '范围参考值',
   LABRESULTID          VARCHAR(50) COMMENT '实验室检查结果id',
   ITEMID               VARCHAR(50) COMMENT '项目ID',
   PRIMARY KEY (TESTITEMID)
);

ALTER TABLE HR_LABORATORYTESTITEM COMMENT '实验室检查项目';

/*==============================================================*/
/* Table: HR_PERSONINFO                                         */
/*==============================================================*/
CREATE TABLE HR_PERSONINFO
(
   PERSONID             NUMERIC(9,0) NOT NULL COMMENT 'PERSONID',
   PERSONNO             VARCHAR(18) NOT NULL COMMENT 'PERSONNO',
   NAME                 VARCHAR(50) NOT NULL COMMENT 'NAME',
   GENDER               VARCHAR(3) NOT NULL COMMENT 'GENDER',
   BIRTHDATE            VARCHAR(8) COMMENT 'BIRTHDATE',
   COUNTRY              VARCHAR(3) COMMENT 'COUNTRY',
   NATIONALITY          VARCHAR(3) COMMENT 'NATIONALITY',
   MARRIAGESTATUS       VARCHAR(3) COMMENT 'MARRIAGESTATUS',
   IDTYPE               VARCHAR(3) NOT NULL COMMENT 'IDTYPE',
   IDNUMBER             VARCHAR(50) NOT NULL COMMENT 'IDNUMBER',
   PHONE                VARCHAR(20) COMMENT 'PHONE',
   CONTACTNAME          VARCHAR(50) COMMENT 'CONTACTNAME',
   CONTACTPHONE         VARCHAR(20) COMMENT 'CONTACTPHONE',
   EMAILADDRESS         VARCHAR(70) COMMENT 'EMAILADDRESS',
   CENSUSREGISTERFLAG   VARCHAR(1) COMMENT 'CENSUSREGISTERFLAG',
   CENSUSADDRESSCODE    VARCHAR(100) COMMENT 'CENSUSADDRESSCODE',
   CENSUSADDRESSNAME    VARCHAR(500) COMMENT 'CENSUSADDRESSNAME',
   CENSUSPOSTCODE       VARCHAR(6) COMMENT 'CENSUSPOSTCODE',
   CURRENTADDRESSCODE   VARCHAR(100) COMMENT 'CURRENTADDRESSCODE',
   CURRENTADDRESSNAME   VARCHAR(500) COMMENT 'CURRENTADDRESSNAME',
   CURRENTPOSTCODE      VARCHAR(6) COMMENT 'CURRENTPOSTCODE',
   COMPANY              VARCHAR(70) COMMENT 'COMPANY',
   HIREDATE             VARCHAR(8) COMMENT 'HIREDATE',
   OCCUPATIONCLASS      VARCHAR(3) COMMENT 'OCCUPATIONCLASS',
   EDUCATIONLEVEL       VARCHAR(3) COMMENT 'EDUCATIONLEVEL',
   INSURANCETYPE        VARCHAR(3) COMMENT 'INSURANCETYPE',
   INSURANCETYPENAME    VARCHAR(3) COMMENT 'INSURANCETYPENAME',
   PAYMETHOD            VARCHAR(3) COMMENT 'PAYMETHOD',
   ABOTYPE              VARCHAR(3) COMMENT 'ABOTYPE',
   RHTYPE               VARCHAR(3) COMMENT 'RHTYPE',
   ALLERGYHISTORY       VARCHAR(2000) COMMENT 'ALLERGYHISTORY',
   RISKFACTORS          VARCHAR(2000) COMMENT 'RISKFACTORS',
   DISABILITYSTATUS     VARCHAR(2000) COMMENT 'DISABILITYSTATUS',
   COMMUNITY            VARCHAR(70) COMMENT 'COMMUNITY',
   COMMUNITYCONTACT     VARCHAR(50) COMMENT 'COMMUNITYCONTACT',
   COMMUNITYCONTACTPHONE VARCHAR(20) COMMENT 'COMMUNITYCONTACTPHONE',
   RESPONSIBLEORGANIZATION VARCHAR(70) COMMENT 'RESPONSIBLEORGANIZATION',
   RESPONSIBLEORGANIZATIONID VARCHAR(10) COMMENT 'RESPONSIBLEORGANIZATIONID',
   RESPONSIBLEDOCTOR    VARCHAR(50) COMMENT 'RESPONSIBLEDOCTOR',
   RESPONSIBLEDOCTORPHONE VARCHAR(20) COMMENT 'RESPONSIBLEDOCTORPHONE',
   CREATEDATE           DATETIME COMMENT 'CREATEDATE',
   CREATEBY             VARCHAR(100) COMMENT 'CREATEBY',
   UPDATEDATE           DATETIME COMMENT 'UPDATEDATE',
   UPDATEBY             VARCHAR(100) COMMENT 'UPDATEBY',
   STATUS               NUMERIC(3,0) NOT NULL DEFAULT 1 COMMENT 'STATUS',
   BLOODTYPE            VARCHAR(4) COMMENT 'BLOODTYPE',
   ARCHIVEDATE          DATETIME COMMENT 'ARCHIVEDATE',
   REMARK               VARCHAR(100) COMMENT 'REMARK',
   PRIMARY KEY (PERSONID)
);

ALTER TABLE HR_PERSONINFO COMMENT 'HR_PERSONINFO';

/*==============================================================*/
/* Table: HR_REF_CODEFILE                                       */
/*==============================================================*/
CREATE TABLE HR_REF_CODEFILE
(
   CODEID               NUMERIC(9,0) NOT NULL COMMENT 'CODEID',
   CODENO               VARCHAR(100) COMMENT 'CODENO',
   STATUS               NUMERIC(3,0) COMMENT 'STATUS',
   CODENAME             VARCHAR(100) COMMENT 'CODENAME',
   CODEVALUE            VARCHAR(4000) COMMENT 'CODEVALUE',
   DESCRIPTION          VARCHAR(500) COMMENT 'DESCRIPTION',
   PRIMARY KEY (CODEID)
);

ALTER TABLE HR_REF_CODEFILE COMMENT 'HR_REF_CODEFILE';

/*==============================================================*/
/* Table: HR_SYS_USER                                           */
/*==============================================================*/
CREATE TABLE HR_SYS_USER
(
   SYSUSERID            VARCHAR(50) NOT NULL COMMENT '系统用户ID',
   EMPLOYEEID           VARCHAR(50) COMMENT '员工ID',
   EMPLOYEENAME         VARCHAR(50) COMMENT '员工姓名',
   PHONENUMBER          VARCHAR(50) COMMENT '员工编号',
   USERNAME             VARCHAR(50) NOT NULL COMMENT '员工系统帐号',
   PASSWORD             VARCHAR(50) NOT NULL COMMENT '用户密码',
   STATE                VARCHAR(50) COMMENT '状态：0 禁用，1 使用',
   REMARK               VARCHAR(2000) COMMENT '备注',
   CREATEUSER           VARCHAR(50) COMMENT '创建人',
   CREATEDATE           DATETIME COMMENT '创建时间',
   UPDATEUSER           VARCHAR(50) COMMENT '修改人',
   UPDATEDATE           DATETIME COMMENT '修改时间',
   OWNERCOMPANYID       VARCHAR(50) COMMENT '所属公司ID',
   OWNERID              VARCHAR(50) COMMENT '所属员工ID',
   OWNERPOSTID          VARCHAR(50) COMMENT '所属岗位ID',
   OWNERDEPARTMENTID    VARCHAR(50) COMMENT '所属部门ID',
   ISMANGER             NUMERIC(38,0) COMMENT '是否为管理员',
   ISFLOWMANAGER        VARCHAR(1) DEFAULT '0' COMMENT '是否是流程管理员',
   ISENGINEMANAGER      VARCHAR(1) DEFAULT '0' COMMENT '是否是引擎管理员',
   PRIMARY KEY (SYSUSERID)
);

ALTER TABLE HR_SYS_USER COMMENT '系统用户表';

/*==============================================================*/
/* Table: HR_SCORE                                              */
/*==============================================================*/
CREATE TABLE HR_SCORE
(
   SCOREID              VARCHAR(32) NOT NULL COMMENT 'GUID',
   PERSONID             CHAR(10) COMMENT '用户ID',
   TABLETYPE            VARCHAR(50) COMMENT '评分量表类型',
   TEMPLATENAME         VARCHAR(50) COMMENT '评分量表名称',
   TOTALSCORE           INT COMMENT '总分',
   GRADE                VARCHAR(50) COMMENT '评分等级',
   DESCRIPTION          VARCHAR(2000) COMMENT '说明',
   CREATETIME           DATETIME COMMENT '创建时间',
   QUESTIONRESULT       TEXT COMMENT '填写的表单',
   PRIMARY KEY (SCOREID)
);

ALTER TABLE HR_SCORE COMMENT '评分量表结果';

/*==============================================================*/
/* Table: HR_SCORETEMPLATE                                      */
/*==============================================================*/
CREATE TABLE HR_SCORETEMPLATE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   TEMPLATETITLE        VARCHAR(50) COMMENT '模板名称',
   TEMPLATEDESCRIPTION  VARCHAR(200) COMMENT '模板描述',
   TEMPLATELABEL        VARCHAR(200) COMMENT '表头提示语',
   SHOWGRADE            INT COMMENT '结果显示方式',
   ISSHOWRESULT         INT COMMENT '是否当页显示结果',
   CREATETIME           DATETIME COMMENT '创建时间',
   CREATEBY             VARCHAR(50) COMMENT '创建者',
   MODIFYTIME           DATETIME COMMENT '修改时间',
   MODIFYBY             VARCHAR(50) COMMENT '修改者',
   QUESTIONSJSON        TEXT COMMENT '问题模板',
   ANSWERSJSON          TEXT COMMENT '答案模板',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_SCORETEMPLATE COMMENT '评分量表模板';

/*==============================================================*/
/* Table: HR_SEEDOCHISICD                                       */
/*==============================================================*/
CREATE TABLE HR_SEEDOCHISICD
(
   INFOID               VARCHAR(50) NOT NULL COMMENT '就诊史疾病详情ID',
   HISTORYID            VARCHAR(50) COMMENT '就诊史ID',
   ICDCODE              VARCHAR(50) COMMENT 'ICD编号',
   ICDNAME              VARCHAR(100) COMMENT 'ICD名称',
   DETAILS              VARCHAR(500) COMMENT '详情',
   ISMAIN               NUMERIC(1,0) COMMENT '是否主诉疾病',
   PRIMARY KEY (INFOID)
);

ALTER TABLE HR_SEEDOCHISICD COMMENT '就诊史疾病详情';

/*==============================================================*/
/* Table: HR_SEEDOCTORHISTORY                                   */
/*==============================================================*/
CREATE TABLE HR_SEEDOCTORHISTORY
(
   HISTORYID            VARCHAR(50) NOT NULL COMMENT '就诊史ID',
   PERSONID             VARCHAR(50) COMMENT '用户ID',
   DIAGNOSISTIME        DATETIME COMMENT '诊断时间',
   DIAGNOSIS            VARCHAR(200) COMMENT '诊断',
   ICD10                VARCHAR(200) COMMENT 'ICD10',
   HOSPITAL             VARCHAR(200) COMMENT '就诊医院',
   DIAGNOSISREPORT      VARCHAR(200) COMMENT '检验报告附件',
   MEDICALHISATTACHMENT VARCHAR(200) COMMENT '病历附件',
   MAINDISEASE          VARCHAR(200) COMMENT '主诉',
   PERSONHISTORY        VARCHAR(200) COMMENT '个人史',
   OBSTETRICALHISTORY   VARCHAR(200) COMMENT '婚育史',
   MENSTRUALHISTORY     VARCHAR(200) COMMENT '月经史',
   PHYSICALEXAM         VARCHAR(1000) COMMENT '体格检查',
   SPECIALISTCASE       VARCHAR(1000) COMMENT '专科情况',
   AUXILIARYEXAM        VARCHAR(1000) COMMENT '辅助检查',
   HISTOLOGICALLY       VARCHAR(1000) COMMENT '组织病理学检查',
   MOLECULARPATHOLOGIC  VARCHAR(1000) COMMENT '分子病理学检查',
   DISEASETYPE          NUMERIC(9,0) COMMENT '肿瘤疾病判断类型',
   ISRELAPSE            NUMERIC(1,0) COMMENT '是否为复发肿瘤',
   T                    VARCHAR(200) COMMENT 'T',
   M                    VARCHAR(200) COMMENT 'M',
   N                    VARCHAR(200) COMMENT 'N',
   POSITION             VARCHAR(200) COMMENT '肿瘤位置',
   LCFQ                 VARCHAR(50) COMMENT '临床分期',
   XBFX                 VARCHAR(200) COMMENT '细胞分型',
   JYFX                 VARCHAR(200) COMMENT '基因分型',
   ZY                   VARCHAR(200) COMMENT '转移',
   REMARK               VARCHAR(1000) COMMENT '备注',
   PRIMARY KEY (HISTORYID)
);

ALTER TABLE HR_SEEDOCTORHISTORY COMMENT '就诊史';

/*==============================================================*/
/* Table: HR_TEMPLATEITEM                                       */
/*==============================================================*/
CREATE TABLE HR_TEMPLATEITEM
(
   ITEMID               VARCHAR(50) NOT NULL COMMENT '模板项ID',
   TEMPLATEID           VARCHAR(50) COMMENT '模板ID',
   ITEMCODE             VARCHAR(20) COMMENT '项代码',
   ITEMNAME             VARCHAR(20) COMMENT '项名称',
   SORT                 INT COMMENT '排序',
   VALUETYPE            VARCHAR(2) COMMENT '类型',
   CODEVALUE            VARCHAR(50) COMMENT '选项值',
   CATETORYID           VARCHAR(50) COMMENT '栏目ID',
   PRIMARY KEY (ITEMID)
);

ALTER TABLE HR_TEMPLATEITEM COMMENT '模板项信息';

/*==============================================================*/
/* Table: HR_TREATMENTHISTORY                                   */
/*==============================================================*/
CREATE TABLE HR_TREATMENTHISTORY
(
   TREATMENTHISTORYID   VARCHAR(50) NOT NULL COMMENT '治疗史ID',
   DISEASEHISTORYID     VARCHAR(50) COMMENT '用户账号',
   TREATMENTTIME        DATETIME COMMENT '治疗时间',
   TREATMENTHOSPITAL    VARCHAR(200) COMMENT '治疗医院',
   TREATMENTTYPE        INT COMMENT '治疗方式',
   OPERATIONTYPE        VARCHAR(2000) COMMENT '手术方式',
   OPERATIONRESLUT      VARCHAR(2000) COMMENT '手术效果',
   RADIOTHERAPYDOSE     VARCHAR(2000) COMMENT '放疗剂量',
   RADIOTHERAPYRESLUT   VARCHAR(2000) COMMENT '放疗效果',
   CHEMOTHERAPYPROJECT  VARCHAR(2000) COMMENT '化疗方案',
   CHEMOTHERAPYDRUG     VARCHAR(2000) COMMENT '化疗药物',
   CHEMOTHERAPYRESLUT   VARCHAR(2000) COMMENT '化疗效果',
   PRIMARY KEY (TREATMENTHISTORYID)
);

ALTER TABLE HR_TREATMENTHISTORY COMMENT '治疗史';

/*==============================================================*/
/* Table: HR_USERGENE                                           */
/*==============================================================*/
CREATE TABLE HR_USERGENE
(
   ID                   VARCHAR(50) NOT NULL COMMENT 'ID',
   USERID               VARCHAR(50) COMMENT 'UserID',
   GENENAME             VARCHAR(50) COMMENT 'GeneName',
   ALLELE1              VARCHAR(200) COMMENT 'Allele1',
   ALLELE2              VARCHAR(200) COMMENT 'Allele2',
   CREATETIME           DATETIME COMMENT 'CreateTime',
   CREATEBY             VARCHAR(50) COMMENT 'CreateBy',
   MODIFYTIME           DATETIME COMMENT 'ModifyTime',
   MODIFYBY             VARCHAR(50) COMMENT 'ModifyBy',
   PRIMARY KEY (ID)
);

ALTER TABLE HR_USERGENE COMMENT '用户基因表';

/*==============================================================*/
/* Table: ICD10                                                 */
/*==============================================================*/
CREATE TABLE ICD10
(
   USERID               VARCHAR(50) NOT NULL COMMENT '用户账号',
   USERNAME             VARCHAR(200) COMMENT '疾病名称',
   PRIMARY KEY (USERID)
);

ALTER TABLE ICD10 COMMENT '疾病表(ICD10)';

ALTER TABLE DUG_DRUGINTERACTIONS ADD CONSTRAINT FK_DRUGINTERACTIONS_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_DRUGSYNONYMS ADD CONSTRAINT FK_REFERENCE_17 FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE DUG_ENZYMEPO ADD CONSTRAINT FK_ENZYMEPO_ENZYMES FOREIGN KEY (ITEMID)
      REFERENCES DUG_ENZYMES (ID);

ALTER TABLE DUG_ENZYMES ADD CONSTRAINT FK_ENZYMES_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_ENZYMESACTIONS ADD CONSTRAINT FK_ENZYMESACTIONS_ENZYMES FOREIGN KEY (ITEMID)
      REFERENCES DUG_ENZYMES (ID);

ALTER TABLE DUG_ENZYMESIDENTIFIER ADD CONSTRAINT FK_ENZYMESIDENTIFIER_ENZYMEPO FOREIGN KEY (ENZYMEPOID)
      REFERENCES DUG_ENZYMEPO (ID);

ALTER TABLE DUG_ENZYMESSYNONYMS ADD CONSTRAINT FK_ENZYMESSYNONYMS_ENZYMEPO FOREIGN KEY (ENZYMEPOID)
      REFERENCES DUG_ENZYMEPO (ID);

ALTER TABLE DUG_EXTERNALIDENTIFIERS ADD CONSTRAINT FK_EXTERNALIDENTIFIERS_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_PRODUCTS ADD CONSTRAINT FK_PRODUCTS_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_TARGETACTIONS ADD CONSTRAINT FK_TARGETACTIONS_TARGETS FOREIGN KEY (ITEMID)
      REFERENCES DUG_TARGETS (ID);

ALTER TABLE DUG_TARGETIDENTIFIER ADD CONSTRAINT FK_TARGETIDENTIFIER_TARGETSPO FOREIGN KEY (TARGETPOID)
      REFERENCES DUG_TARGETSPO (ID);

ALTER TABLE DUG_TARGETS ADD CONSTRAINT FK_TARGETS_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_TARGETSPO ADD CONSTRAINT FK_TARGETSPO_TARGETS FOREIGN KEY (ITEMID)
      REFERENCES DUG_TARGETS (ID);

ALTER TABLE DUG_TRANSPORTERACTIONS ADD CONSTRAINT FK_TRANSPORTERACTIONS_TRANSPORTERS FOREIGN KEY (ITEMID)
      REFERENCES DUG_TRANSPORTERS (ID);

ALTER TABLE DUG_TRANSPORTERIDENTIFIER ADD CONSTRAINT FK_TRANSPORTERIDENTIFIER_TRANSPORTERSPO FOREIGN KEY (TRANSPORTERPOID)
      REFERENCES DUG_TRANSPORTERSPO (ID);

ALTER TABLE DUG_TRANSPORTERS ADD CONSTRAINT FK_TRANSPORTERS_DRUG FOREIGN KEY (DRUGBANKID)
      REFERENCES DUG_DRUG (DRUGBANKID);

ALTER TABLE DUG_TRANSPORTERSPO ADD CONSTRAINT FK_TRANSPORTERSPO_TRANSPORTERS FOREIGN KEY (ITEMID)
      REFERENCES DUG_TRANSPORTERS (ID);

ALTER TABLE HPN_TEMPLATE ADD CONSTRAINT FK_HPN_TEMPLATE_HPN_TEMPLATETYPE FOREIGN KEY (HPNTYPE)
      REFERENCES HPN_TEMPLATETYPE (ID);

ALTER TABLE HPN_USERS ADD CONSTRAINT FK_HPN_USERS_HPN_USERTYPE FOREIGN KEY (HPNTYPE)
      REFERENCES HPN_USERTYPE (ID);

ALTER TABLE HPN_USERS ADD CONSTRAINT FK_HPN_USERS_HPN_VIPDESC FOREIGN KEY (VIPLEVEL)
      REFERENCES HPN_VIPDESC (ID);

ALTER TABLE HR_TREATMENTHISTORY ADD CONSTRAINT FK_REFERENCE_12 FOREIGN KEY (DISEASEHISTORYID)
      REFERENCES HR_DISEASEHISTORY (DISEASEHISTORYID) ON DELETE RESTRICT ON UPDATE RESTRICT;

