SELECT t.function_type, t.action_type, t.remark, t.action_user,
       date_format(action_time,'%Y-%m-%d %H:%i:%s.%f') AS action_time
  FROM log_system_action t
 WHERE action_time BETWEEN @from_dt AND @to_dt
   AND action_user LIKE @cond_1
   AND t.action_type = 'FormDIOSetting'
 ORDER BY action_time;