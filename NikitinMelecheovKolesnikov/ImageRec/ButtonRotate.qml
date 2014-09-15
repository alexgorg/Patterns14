import QtQuick 2.0

Rectangle {
    id: buttonRotate
    property int size: 25
    property string source: ""
    height: size
    width: size

    signal clicked

    Rectangle{
        id: imageRectangle
        anchors.fill: parent

        border.color: "darkgray"
        border.width: 2
        radius: 4

        Behavior on border.color{
            PropertyAnimation{
                duration: 200
                easing.type: Easing.InOutQuad
            }
        }

        Image{
            id: image
            anchors.fill: parent
            anchors.leftMargin: 4
            anchors.rightMargin: 4
            anchors.topMargin: 4
            anchors.bottomMargin: 4
            source: buttonRotate.source
            Behavior on rotation {
                PropertyAnimation{
                    duration: 200
                    easing.type: Easing.InOutQuad
                }
            }

        }
    }

    MouseArea{
        anchors.fill: parent
        enabled: parent.opacity == 1
        hoverEnabled: enabled
        onEntered: {
            imageRectangle.border.color = "orange"
        }
        onExited: {
            imageRectangle.border.color = "darkgray"
        }
        onClicked: {
            if(parent.state === "secondState")
                parent.state = "firstState"
            else
                parent.state = "secondState"
            parent.clicked()
        }
    }

    state: "firstState"
    states: [
        State {
            name: "firstState"
            PropertyChanges {
                target: image
                rotation: 0
            }
        },
        State {
            name: "secondState"
            PropertyChanges {
                target: image
                rotation: 180
            }
        }

    ]
}
