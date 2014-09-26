import QtQuick 2.0

Rectangle {
    id:sliderText
    property string text
    property real value: sliderItem.x / (sliderRectangle.width - sliderItem.width)

    Column{
        spacing: 4
        Rectangle{
            id: textRectangle
            width: text.width + 20
            height: text.height + 10

            Text{
                id:text
                anchors.centerIn: parent
                text:sliderText.text
                color: sliderText.enabled ? "black" : "gray"
            }
        }
        Row{
            spacing: 4
            Rectangle{
                id:sliderRectangle
                width: sliderText.width - 30
                height: 10
                Rectangle{
                    width: parent.width
                    height: 3

                    border.color: sliderText.enabled ? "darkgray" : "lightgray"
                    border.width: 4
                    radius: 4

                    Rectangle{
                        id: sliderItem
                        x: parent.width/2 - width/2
                        y: -3
                        width: 10
                        height: 10

                        border.color: sliderText.enabled ? "black" : "gray"
                        border.width: 2
                        radius: 2

                        color: "orange"
                    }
                    MouseArea{
                        anchors.fill: parent
                        property bool insideSliderItem: false
                        property int offsetX
                        onPressed: {
                            if(mouseX >= sliderItem.x && mouseX <= sliderItem.x + sliderItem.width &&
                                    mouseY >= sliderItem.y && mouseY <= sliderItem.y + sliderItem.height){
                                insideSliderItem = true;
                                offsetX = mouseX - sliderItem.x;
                            }
                        }
                        onReleased: {
                            insideSliderItem = false;
                        }

                        onMouseXChanged: {
                            if(pressed && insideSliderItem){
                                sliderItem.x = mouseX - offsetX;
                                if(sliderItem.x < 0)
                                    sliderItem.x = 0;
                                if(sliderItem.x >= sliderRectangle.width - sliderItem.width)
                                    sliderItem.x = sliderRectangle.width - sliderItem.width;
                            }
                        }
                    }
                }
            }

            Rectangle{
                y:sliderRectangle.y - (height - 3)/2
                width: 26
                height: valueText.height + 4
                border.color: sliderText.enabled ? "darkgray" : "lightgray"
                border.width: 2
                radius: 4
                Text{
                    id: valueText
                    anchors.centerIn: parent
                    text: Math.round(100*sliderText.value)
                    color: sliderText.enabled ? "black" : "gray"
                }
            }
        }
    }
}
