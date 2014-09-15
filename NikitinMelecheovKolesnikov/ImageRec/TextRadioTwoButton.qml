import QtQuick 2.0

Rectangle {
    id: textRadioTwoButton

    property string text: ""
    property string button1Text: ""
    property string button2Text: ""

    property int checkIndex: 1

    property int sizeButton: 15
    property int textRectangleWidth:100
    property int textButtonWidth:45

    height: sizeButton + 4 + sizeButton
    width: textRectangleWidth + 4 + sizeButton + 4 + textButtonWidth

    Rectangle{
        id: textRectangle
        height: text.height + 10
        width: textRadioTwoButton.textRectangleWidth

        x:4
        y: (textRadioTwoButton.height - height)/2

        Text{
            id: text
            y: (parent.height - height)/2
            text: textRadioTwoButton.text
        }
    }

    Rectangle{
        id: button1Rectangle
        height: textRadioTwoButton.sizeButton
        width: textRadioTwoButton.sizeButton + 4 + textRadioTwoButton.textButtonWidth

        x: textRectangle.width + 4
        y: (textRadioTwoButton.height - textRadioTwoButton.sizeButton * 2 + 4)/2

        Rectangle {
            id: button1Check
            height: parent.height
            width: height
            border.color: "darkgray"
            border.width: 2
            radius: width/2

            Rectangle{
                width: parent.width - 10
                height: width
                radius: width/2
                anchors.centerIn: parent
                color: textRadioTwoButton.checkIndex == 1 ? "black" : "white"

                Behavior on color {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }
            }

            Behavior on border.color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }
        }
        Text{
            id: button1Text
            x: button1Check.width + 4
            y: (parent.height - height) / 2
            text: textRadioTwoButton.button1Text
        }

        MouseArea{
            id: button1Mouse
            anchors.fill: parent
            enabled: textRadioTwoButton.opacity == 1
            hoverEnabled: enabled

            onEntered: {
                button1Check.border.color = "orange"
            }

            onExited: {
                button1Check.border.color = "darkgray"
            }

            onClicked: {
                if(textRadioTwoButton.checkIndex != 1){
                    textRadioTwoButton.checkIndex = 1;
                }
            }

        }
    }

    Rectangle {
        id: button2Rectangle
        height: textRadioTwoButton.sizeButton
        width: textRadioTwoButton.sizeButton + 4 + textRadioTwoButton.textButtonWidth


        x: textRectangle.width + 4
        y: button1Rectangle.y + button1Rectangle.height + 4

        Rectangle {
            id: button2Check
            height: parent.height
            width: height
            border.color: "darkgray"
            border.width: 2
            radius: width/2

            Rectangle{
                width: parent.width - 10
                height: width
                radius: width/2
                anchors.centerIn: parent
                color: textRadioTwoButton.checkIndex == 2 ? "black" : "white"

                Behavior on color {
                    PropertyAnimation{
                        duration: 200
                        easing.type: Easing.InOutQuad
                    }
                }
            }

            Behavior on border.color {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }
        }
        Text{
            id: button2Text
            x: button2Check.width + 4
            y: (parent.height - height) / 2
            text: textRadioTwoButton.button2Text
        }

        MouseArea{
            id: button2Mouse
            anchors.fill: parent
            enabled: textRadioTwoButton.opacity == 1
            hoverEnabled: enabled

            onEntered: {
                button2Check.border.color = "orange"
            }

            onExited: {
                button2Check.border.color = "darkgray"
            }

            onClicked: {
                if(textRadioTwoButton.checkIndex != 2) {
                    textRadioTwoButton.checkIndex = 2;
                }
            }

        }
    }
}
