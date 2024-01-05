for /r ".\views" %%F in (*.sql) do @type "%%F" >>CreateAllViews.sql
