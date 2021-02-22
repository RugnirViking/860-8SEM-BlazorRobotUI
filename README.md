# RobotUI
![Preview image showing fading text with icons to show user and robotconversation](https://i.imgur.com/l9dvdfA.png)
 This is the Robot UI for group 860's 8th semester project. It features signalR hooked up to a blazor frontend

**How to use:**
 The ui can be run on a windows machine using IIS express. It opens a web server, which can be connected to on port 80, and a websockets server for reciving messages from ros, which is opened on port 8765. 

**Websocket message format:**
<msg_type>||<msg_text>
Message types sent via websocket must be prefixed with a message type, followed by a delimiter, then the text. Valid message types are as follows:
* "usr" - a message sent to display detected user speech. Displays in green with a user icon
* "rob" - a message sent to display robot speech. Displays in light blue with a robot icon
* "dbg" - a message showing debug information. Displays in blue with an information symbol icon
* "err" - a message showing an error. Displays in red with a warning symbol
