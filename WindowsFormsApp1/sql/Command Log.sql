SELECT m.txn_id, m.node_name, m.node_type,m.cmd_type,
       d.return_type, 
		 date_format(d.receive_time,'%Y-%m-%d %H:%i:%s.%f') AS receive_time, 
		 d.raw_data, d.return_value, m.txn_status,  
		 date_format(m.txn_start_time,'%Y-%m-%d %H:%i:%s.%f') AS txn_start_time, 
		 date_format(m.txn_end_time,'%Y-%m-%d %H:%i:%s.%f') AS txn_end_time, 
		 m.txn_method, m.txn_position,
		 m.txn_slot, m.txn_arm, m.txn_value, m.script_name, m.form_name
  FROM log_cmd_txn m, log_cmd_txn_detail d
 WHERE m.txn_id = d.txn_id
   AND m.txn_start_time BETWEEN @from_dt AND @to_dt
   AND node_type LIKE @cond_1
   AND node_name LIKE @cond_2
 ORDER BY m.txn_start_time, m.txn_id, d.receive_time