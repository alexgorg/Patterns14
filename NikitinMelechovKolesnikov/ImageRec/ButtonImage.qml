import QtQuick 2.0

Rectangle {
    id:buttonImage
    property string source
    property string text:""
    property real size: 30

    width: size + ((text === "") ? 0 : (4*2 + buttonImageText.width))
    height: size

    onEnabledChanged: {
        buttonImageRectangle.border.color = buttonImage.enabled ? "darkgray" : "lightgray";
        buttonImageText.color = buttonImage.enabled ? "black" : "gray";
    }

    signal clicked

    Rectangle{
        id: buttonImageRectangle

        border.color: buttonImage.enabled ? "darkgray" : "lightgray"
        border.width: 2
        radius: 4

        width: parent.size
        height:parent.size

        Behavior on border.color {
            PropertyAnimation{
                duration: 200
                easing.type: Easing.InOutQuad
            }
        }

        Image{
            id: buttonImageImage


            anchors.fill: parent
            anchors.leftMargin: 4
            anchors.rightMargin: 4
            anchors.topMargin: 4
            anchors.bottomMargin:4

            source: buttonImage.source
        }
    }
    Text{
        id: buttonImageText
        text:buttonImage.text
        x: buttonImageRectangle.width + buttonImageRectangle.x + 4
        y: (buttonImage.height - height)/2
        color: buttonImage.enabled ? "black" : "gray"
    }

    MouseArea{
        id: buttonImageMouse

        enabled: parent.opacity == 1

        anchors.fill: parent
        hoverEnabled: enabled
        onClicked: {
            parent.clicked()
        }
        onPressed: {
            buttonImageImage.scale = 0.9
            buttonImageText.color = "#777777"
        }
        onReleased: {
            buttonImageImage.scale = 1
            buttonImageText.color = "#000000"

        }

        onEntered: {
            if(!pressed)
                buttonImageRectangle.border.color = "orange"
            else
                buttonImageRectangle.border.color = "darkgray"
        }
        onExited: {
            buttonImageRectangle.border.color = "darkgray"
        }
    }
}
