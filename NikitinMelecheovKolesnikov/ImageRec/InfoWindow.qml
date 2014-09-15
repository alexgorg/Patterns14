import QtQuick 2.0
import QtQuick.Window 2.0

Window {
    width: 300
    height: 200

    Rectangle {
        anchors.fill: parent;
        anchors.margins: 4
        border.color: "darkgray"
        border.width: 4
        radius: 4

        Text {
            anchors.top: parent.top
            anchors.left: parent.left
            anchors.margins: 8
            font.pixelSize: 20
            text: "Автор: А.Никитин\n" + "e-mail: nikialkesey@gmail.com\n" +
                  "Версия: 1.0";
        }

        Text {
            anchors.horizontalCenter: parent.horizontalCenter
            anchors.bottom: parent.bottom
            anchors.margins: 8
            text: "Барнаул 2014"
        }
    }
}

