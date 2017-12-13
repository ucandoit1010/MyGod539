SELECT NUM , SUM(COUNT1) AS 'COUNT1' FROM (
SELECT open_num_1 AS 'NUM',COUNT(open_num_1) AS 'COUNT1' FROM History WHERE substr(open_no,1,3) = @Year GROUP BY open_num_1  
union ALL
SELECT open_num_2 AS 'NUM',COUNT(open_num_2) AS 'COUNT1' FROM History WHERE substr(open_no,1,3) = @Year GROUP BY open_num_2  
union ALL
SELECT open_num_3 AS 'NUM',COUNT(open_num_3) AS 'COUNT1' FROM History WHERE substr(open_no,1,3) = @Year GROUP BY open_num_3  
union ALL
SELECT open_num_4 AS 'NUM',COUNT(open_num_4) AS 'COUNT1' FROM History WHERE substr(open_no,1,3) = @Year GROUP BY open_num_4 
union ALL
SELECT open_num_5 AS 'NUM',COUNT(open_num_5) AS 'COUNT1' FROM History WHERE substr(open_no,1,3) = @Year GROUP BY open_num_5 )
GROUP BY NUM;