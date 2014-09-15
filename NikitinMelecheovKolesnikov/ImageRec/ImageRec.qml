import QtQuick 2.2
import QtQuick.Controls 1.1
import QtQuick.Window 2.0
import "ParticleData.js" as ParticleData

Window {
    id: mainWindow
    title: qsTr("Particle Recognition by A.Nikitin")
    width: 800
    height: 600


    MainWindow {
        particleData: ParticleData;
    }

    ChangeScaleWindow {
        id: changeScaleWindow
        width: 800
        height:400
        onAccepted: {
            imageWindow.updateCalcParam(menuTab.tdelay, menuTab.horizontalAngle,
                                        menuTab.magicParam,changeScaleWindow.currentScale);
        }
    }

    GraphWindow {
        id: graphWindow
        width: 800
        height: 400
        particleData: ParticleData

    }

    InfoWindow {
        id: infoWindow
    }
}
