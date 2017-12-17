SELECT open_no,open_date,open_num_1,open_num_2,open_num_3,open_num_4,open_num_5 
FROM History WHERE substr(open_no,1,3) = @Year;