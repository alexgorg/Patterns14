var particleInfo = [];
var bufParticle;
var width, height;

function removeParticle(index){
    if(particleInfo[index] === undefined)
        return;
    particleInfo.splice(index,1);
}

function addParticle(particle) {
    particleInfo.push({i:particle.i, j: particle.j, width: particle.width, height: particle.height,
                               angle: particle.angle, velocity: particle.velocity});
}
