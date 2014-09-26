import QtQuick 2.0

Rectangle {
    id: checkBoxText
    property bool checked
    property string text
    property int size: 15

    width: size + 4 + textRectangle.width
    height: size

    Rectangle{
        id: checkRectangle
        width: size
        height: size

        border.color: "darkgray"
        border.width: 2
        radius: 4

        Behavior on border.color {
            PropertyAnimation{
                duration: 200
                easing.type: Easing.InOutQuad
            }
        }

        Rectangle {
            id: check
            anchors.centerIn: parent
            width: size - 10
            height: size - 10
            radius: 1
            color: "white"
        }
    }
    Rectangle{
        id: textRectangle
        width: textCheckBox.width + 4
        height: textCheckBox.height + 10
        x: checkRectangle.width + 4
        y: (checkBoxText.height - height)/2
        Text{
            id: textCheckBox
            anchors.centerIn: parent
            text: checkBoxText.text
        }
    }
    MouseArea{

        enabled: parent.opacity == 1

        anchors.fill: parent
        hoverEnabled: enabled

        onPressed: {
            textCheckBox.color = "#777777"
        }

        onReleased: {
            textCheckBox.color = "black"
        }

        onEntered: {
            checkRectangle.border.color = "orange"
        }

        onExited: {
            checkRectangle.border.color = "darkgray"
        }

        onClicked: {
            if(parent.state == "checked")
                parent.state = "notchecked";
            else if(parent.state == "notchecked")
                parent.state = "checked";
        }
    }

    state: "notchecked"
    states: [
        State {
            name: "checked"
            PropertyChanges {
                target: check
                color: "black"
            }
            PropertyChanges{
                target: checkBoxText
                checked: true
            }
        },
        State {
            name: "notchecked"
            PropertyChanges {
                target: check
                color: "white"
            }
            PropertyChanges{
                target: checkBoxText
                checked: false
            }
        }
    ]
    transitions: [
        Transition {
            from: "checked"
            to: "notchecked"
            PropertyAnimation{
                target: check
                property: "color"
                duration: 200
                easing.type: Easing.InOutQuad
            }
        },
        Transition {
            from: "notchecked"
            to: "checked"
            PropertyAnimation{
                target: check
                property: "color"
                duration: 200
                easing.type: Easing.InOutQuad
            }
        }
    ]
}
