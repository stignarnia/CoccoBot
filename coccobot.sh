#!/bin/sh
while [ 1 != 0 ]
do
	pkill CoccoBotLinux
	sleep 5
	rclone copy --update --verbose --transfers 3 --checkers 3 --contimeout 60s --timeout 300s --retries 3 --low-level-retries 10 --stats 1s "/home/ubuntu/" "google-drive:coccobot"
	rclone copy --update --verbose --transfers 3 --checkers 3 --contimeout 60s --timeout 300s --retries 3 --low-level-retries 10 --stats 1s "/var/www/html/" "google-drive:website"
	wget https://github.com/stignarnia/CoccoBot/releases/latest/download/CoccoBotLinux -O /home/ubuntu/CoccoBotLinux
	chmod +x CoccoBotLinux
	screen ./CoccoBotLinux
	sleep 1d
done
