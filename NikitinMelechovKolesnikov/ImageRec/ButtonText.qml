import QtQuick 2.0

Rectangle {

    id: buttonMain

    property string text

    signal clicked

    border.color: enabled ? "darkgray" : "lightgray"
    border.width: 2
    radius: 4

    width: buttonText.width + 20
    height: buttonText.height + 10

    Behavior on border.color {
        PropertyAnimation{
            duration: 200
            easing.type: Easing.InOutQuad
        }
    }

    Text{
        id: buttonText
        anchors.centerIn: parent
        color: parent.enabled ? "black" : "gray"
        text: parent.text
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
            buttonText.color = "#777777"
        }
        onReleased: {
            buttonText.color = "#000000"
        }

        onEntered: {
            parent.border.color = "orange"
        }
        onExited: {
            parent.border.color = "darkgray"
        }
    }
}
