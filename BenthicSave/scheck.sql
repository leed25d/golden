define slist='107794','109053','109514','110270','110320','111380','111896','1034764','1034853','1036628','1039864','1040510','1044214','1045237','1045677','1045832','1048088','1048436','1049337','1052265','1053821','1054508','1056923','1057164','1058193','1058775','1059195','1060558','1060562','1062585','1062586','1062627','1062633','1062637','1062664','1062685','1062687','1062689','1062718','1062741','1062742','1062743','1062762','1062767','1062782','10050746','10052020','10052026','10053641','10057180','20003404','20008691','20011175','20012314','20012639','20015226','20015326','20016231','20016863','20016950','20017857','20018022','20018289','20018394','20019042','20019098','20019613','20019908','20020039','30000026','30001279','30001807','30001969','30002455','30002732','30002904','30002962','30002964','30002976','30003068','30003336','30003731','30003745','30003962','30004454','30005236','30005376','30005584','30005690','30006164','30006196','30006249','30006321','30006426','30006437','30006468','30006569','30006605','30006994','30007185','30007786','30007885','30008160','30008198'

select * from common.entity_student where student_number in (&slist);
