class SimpleGainModule : Fabric.ModularSynth.Module
{
    // Local gain parameter
    float gain = 1.0f;

    // Declear Module input/output pins
    Fabric.ModularSynth.ControlInputPin gainInput = new Fabric.ModularSynth.ControlInputPin(1.0f, 0.0f, 1.0f);
    Fabric.ModularSynth.AudioInputPin audioInput = new Fabric.ModularSynth.AudioInputPin();
    Fabric.ModularSynth.AudioOutputPin audioOutput = new Fabric.ModularSynth.AudioOutputPin();

    public SimpleGainModule()
    {
        // Register pins with module
        RegisterPin(gainInput, "Gain");
        RegisterPin(audioInput, "Input");
        RegisterPin(audioOutput, "Output");
    }

    public override void OnControlPinsUpdated() 
    {
        // gain input pin has been updated, NOTE: This is only called when a value has changed
        gain = (float)gainInput.GetValue();
    }

    public override void OnAudioPinsUpdate() 
    {
        // Check audio input/output pins are connected
        if (audioInput.IsConnected() != true || audioOutput.HasBuffer() != true)
        {
            return;
        }

        // Get audio pin buffers
        Fabric.ModularSynth.AudioBuffer inputBuffer = audioInput.GetBuffer();
        Fabric.ModularSynth.AudioBuffer outputBuffer = audioOutput.GetBuffer();

        // Scale input buffer
        inputBuffer.Scale(gain);

        // Pass it to the output buffer
        inputBuffer.CopyToBuffer(outputBuffer);
    }
}

class SimpleGainFactory : Fabric.ModularSynth.ModuleFactory
{
    public override Fabric.ModularSynth.Module CreateInstance()
    {
        return new SimpleGainModule() as Fabric.ModularSynth.Module;
    }

    public override string Name()
    {
        // Name of the module
        return "SimpleGain";
    }
}

public class SimpleGain : Fabric.ModularSynth.ModularSynthFactoryUnity
{
    public override void RegisterFactory()
    {
        // Register module with the modular synth manager
        Fabric.ModularSynth.ModularSynthManager.Instance.RegisterComponentFactory(new SimpleGainFactory());
    }
}
