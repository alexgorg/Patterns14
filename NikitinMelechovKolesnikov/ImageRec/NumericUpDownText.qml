import QtQuick 2.2

Rectangle {
    id: numericUpDownText
    property string text: ""
    property string infoText: ""
    property int minValue:0
    property int maxValue:100
    property int value: 0
    property int valueWidth: 60

    height: valueRectangle.height

    Rectangle{
        id: textRectangle

        height: text.height + 10
        width: numericUpDownText.width - numericUpDownText.valueWidth - 4

        y: (numericUpDownText.height - height)/2

        Text{
            id: text
            x: 4
            y: (textRectangle.height - height) / 2
            text:numericUpDownText.text
        }
    }
    Rectangle{
        id: valueRectangle

        height: textEdit.height + 10
        width: numericUpDownText.valueWidth - infoText.width - 4

        x: textRectangle.width + 4
        y: (numericUpDownText.height - height)/2

        border.color: "darkgray"
        border.width: 2

        TextInput{
            id: textEdit
            y: (parent.height - height)/2
            x: 4

            text: numericUpDownText.value
            cursorVisible: false
            maximumLength: 3
            cursorPosition: 0

            inputMask: "###"

//            onAccepted: {
//                console.log("OK")
//                if(textEdit.text === "0" || textEdit.text === ""){
//                    textEdit.text = "1";
//                    numericUpDownText.value = 1
//                } else if(textEdit.text){
//                    numericUpDownText.value = textEdit.text
//                }
//            }
        }

        Rectangle{
            id: up
            border.color: "darkgray"
            border.width: 2
            anchors.right: parent.right
            anchors.top: parent.top
            height: parent.height/2
            width: height

            Image{
                source: "img/upMini.png"
                anchors.fill: parent
                anchors.margins: 2
            }

            MouseArea{
                id: mouseUp
                anchors.fill: parent
                enabled: numericUpDownText.opacity == 1
                hoverEnabled: enabled

                function clickedUpEvent(){
                    numericUpDownText.value += 1
                    if(numericUpDownText.value > maxValue)
                        numericUpDownText.value = maxValue
                }

                onEntered: {
                    up.border.color = "orange"
                }

                onExited: {
                    up.border.color = "darkgray"
                }

                onPressed: {
                    clickedUpEvent();
                }

                onPressAndHold: {
                    mouseUpTimer.running = true
                }

                onReleased: {
                    mouseUpTimer.running = false
                }

                Timer{
                    id: mouseUpTimer
                    running: false
                    repeat: true
                    interval: 10
                    onTriggered: mouseUp.clickedUpEvent()
                }
            }
        }
        Rectangle{
            id: down
            border.color: "darkgray"
            border.width: 2
            anchors.right: parent.right
            anchors.bottom: parent.bottom
            height: parent.height/2
            width: height

            Image {
                source: "img/upMini.png"
                rotation: 180
                anchors.fill: parent
                anchors.margins: 2
            }

            MouseArea{
                id: mouseDown
                anchors.fill: parent
                enabled: numericUpDownText.opacity == 1
                hoverEnabled: enabled

                function clickedDownEvent(){
                    numericUpDownText.value -= 1
                    if(numericUpDownText.value < minValue)
                        numericUpDownText.value = minValue
                }

                onEntered: {
                    down.border.color = "orange"
                }

                onExited: {
                    down.border.color = "darkgray"
                }

                onPressed: {
                    clickedDownEvent();
                }

                onPressAndHold: {
                    mouseDownTimer.running = true
                }

                onReleased: {
                    mouseDownTimer.running = false
                }

                Timer{
                    id: mouseDownTimer
                    running: false
                    repeat: true
                    interval: 10
                    onTriggered: mouseDown.clickedDownEvent()
                }
            }
        }
    }
    Text{
        id: infoText
        x: valueRectangle.x + valueRectangle.width + 4
        y: (numericUpDownText.height - height)/2
        text: numericUpDownText.infoText
    }
}
