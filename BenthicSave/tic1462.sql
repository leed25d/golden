define jcmid_list = "2001338,2001340,2001342,2001344,2001346,2001348,2001350,2001352,2001354,2001356,2001296,2001298,2001300,2001310,2001314,2001336,2001308,2001320,2001318,2001316";

select JCM_ID, ACTIVITY_GROUP from maa.ph_jcm where jcm_id in (&jcmid_list);

update maa.ph_jcm set activity_group=1 where jcm_id in (&jcmid_list);

