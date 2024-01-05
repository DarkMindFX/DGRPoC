for /r ".\tables" %%F in (*.sql) do @type "%%F" >>CreateAllTables.sql
