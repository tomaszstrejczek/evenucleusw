gci . *.js -rec | Where {$_.FullName -notlike "*\node_modules\*" -and $_.FullName -notlike "*config.js" -and $_.FullName -notlike "*conf.js"} | Remove-Item
gci . *.js.map -rec | Where {$_.FullName -notlike "*\node_modules\*"} | Remove-item
