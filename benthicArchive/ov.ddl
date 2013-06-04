REM Note that this DDL is not guaranteed to include
REM   all properties of the original object.  It will also
REM   not include CLUSTER or PARTITION information.  Please
REM   report any other problems to support@benthicsoftware.com
CREATE TABLE NURSING.OFFICE_VISITS (
	SID                    NUMBER,
	EMPID                  NUMBER,
	TIME                   NUMBER(10),
	VISIT_DATE             DATE,
	TIME_IN                VARCHAR2(5),
	TIME_OUT               VARCHAR2(5),
	COMPLAINT              VARCHAR2(100),
	OTHER_COMPLAINT        VARCHAR2(100),
	TEMPERATURE            VARCHAR2(6),
	CARE_GIVEN             VARCHAR2(100),
	CARE_GIVEN_OTHER       VARCHAR2(100),
	CARE_ASSESSMENT_NOTES  VARCHAR2(100),
	CARE_ASSESSMENT_TYPE   VARCHAR2(100),
	CARE_TRANSPORT         VARCHAR2(100),
	CARE_VITAL_NOTES       VARCHAR2(100),
	PARENT_CONTACT         VARCHAR2(100),
	DISPOSITION            VARCHAR2(100),
	COMMENTS               VARCHAR2(1000),
	LOCKED                 NUMBER(1),
	PARENT_CONTACT_OTHER   VARCHAR2(255),
	REFERRAL               VARCHAR2(255),
	RECORDED_BY            NUMBER,
	DATA_ENTRY             NUMBER(1),
	DISPOSITION_OTHER      VARCHAR2(255),
	REP_SITE               VARCHAR2(5),
	REP_DATE               DATE,
	VISIT_ID               NUMBER,
	BILLED                 DATE,
	SCHOOL_LOC             VARCHAR2(7),
	APPROVED               NUMBER(1),
	PLAN                   NUMBER(1),
	COMPLAINT_MINUTES      NUMBER,
	SCHOOL_ID              NUMBER,
	CONTACT_PROVIDER       VARCHAR2(255),
	CONTACT_AGENCY         VARCHAR2(255),
	CONTACT_SCHOOL         VARCHAR2(255),
	CONTACT_OTHER          VARCHAR2(255),
	DISPOSITION_PROVIDER   VARCHAR2(255),
	DISPOSITION_AGENCY     VARCHAR2(255),
	DISPOSITION_SCHOOL     VARCHAR2(255),
	DISPOSITION_ONSITE     VARCHAR2(255),
	OLD_INTAKE_MINUTES     NUMBER,
	COMPLAINT_ASSESSMENT   NUMBER);

ALTER TABLE NURSING.OFFICE_VISITS ADD (
	CONSTRAINT OFFICE_VISITS_PK PRIMARY KEY (VISIT_ID));

CREATE INDEX NURSING.OFFICE_VISITS_EMPID ON NURSING.OFFICE_VISITS (EMPID);

CREATE INDEX NURSING.OFFICE_VISITS_RECORDED_BY ON NURSING.OFFICE_VISITS (RECORDED_BY);

CREATE INDEX NURSING.OFFICE_VISITS_VISIT_DATE ON NURSING.OFFICE_VISITS (VISIT_DATE);

CREATE INDEX NURSING.OFFICE_VISITS_SCHOOL_LOC ON NURSING.OFFICE_VISITS (SCHOOL_LOC);

CREATE INDEX NURSING.OFFICE_VISITS_SID ON NURSING.OFFICE_VISITS (SID);

CREATE INDEX NURSING.OFFICE_VISITS_TIME ON NURSING.OFFICE_VISITS (TIME);

CREATE INDEX NURSING.OFFICE_VISIT_SCHOOL_IX ON NURSING.OFFICE_VISITS (SCHOOL_ID);

CREATE OR REPLACE TRIGGER "NURSING".NURSING."OFFICE_VISITS_INS_TRIGGER" 
before insert on nursing.office_visits
for each row
begin
  if not dbms_reputil.from_remote and :new.visit_id is null then
    select nursing.office_visits_visit_id_seq.nextval into :new.visit_id from dual;
    :new.rep_site := substr(dbms_reputil.global_name, 1, 5);
    :new.rep_date := sysdate;
  end if;
end;
/
CREATE OR REPLACE TRIGGER "NURSING".NURSING."OFFICE_VISITS_UPD_TRIGGER" 
before update on nursing.office_visits
for each row
begin
  if not dbms_reputil.from_remote then
    :new.rep_site := substr(dbms_reputil.global_name, 1, 5);
    :new.rep_date := sysdate;
  end if;
end;
/


