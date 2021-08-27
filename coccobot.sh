#!/bin/sh
while [ 1 != 0 ]
do
	pkill CoccoBotLinux
	sleep 5
	wget https://github.com/stignarnia/CoccoBot/releases/latest/download/CoccoBotLinux -O /home/ubuntu/CoccoBotLinux
	chmod +x CoccoBotLinux
	screen ./CoccoBotLinux
	sleep 1d
done
