
# Welcome to the Mapper System

The mapper system is an extensible dynamic mapping system using .NET Core. The system maps  data between an  internal system C# data models 
and an external data models. It handles both the conversion of the internal 
models to partner-specific formats and the mapping of incoming partner data to the internal models.

# Utilities

The following utilities and frameworks have been used to implement the mapper system:
- .NET Core 8
- Logging component of [NLog](https://github.com/NLog/NLog) 
- [Fluentvalidation](https://github.com/FluentValidation/FluentValidation) to validate the models

# Solution

The mapper system can be viewed as a mediator between the internal and external systems. By knowing the object type of the source/input data, the appropriate strategy is used to transform it into the associated data. It avoids a direct dependency between the two systems and builds an overarching logic to map the models. It's the only component that knows the source and the target clients.

![mapperchart](./resources/MapperSystemDiagram.jpg)

# Use Case
The  mapper handles 2 scenarios. In the first case the system receives a payload object from an external party. This should be converted in the associated model based on the internal or external structure. The payload can be a JSON, XML or a similar data structure. In the 2nd. scenario the mapper converts an external  object into the corresponding object. The following flowchart describes both scenarios.

![mapperflowchart](./resources/MapperFlowchart.jpg)

## Payload Scenario

The system provides a specific handler for the payload:

  - XmlPayloadHandler converts XML data into a .Net object.
  - JsonPayloadHandler recognizes the JSON structure and converts the input data into a .Net object.


## .NET Model Object Scenario

In this case, the mapper system validates the source model before converting it to the associated model. To accomplish this task, a predefined configuration or profile is required. Only one profile is required for the association between two different objects. The mapping is always reversible, meaning no additional profile is required to map the models from the target to the source client.


# System Architecture

The system consists of
- the ModelMapper.Core component, that provides the MapHandler. It depends on 
- the internal system (in this case DIRS21.Model component) contains associated models to 
- the external/third system (ExternalCleint.DataModel). It provides models for all clients like google.
- the DIRS21.Core component contains  base model for the validation logic of all models
  
  
![mapperflowchart](./resources/mapperdiagram.png)


## MapHandler

The MapHandler is the controller of the main system. This depends on the property converter, model profile and logger. It provides mapping methods for the payload object and other object types provided by the internal and external system.

## Mapping Profile

The mapping configuration between models is controlled by a profile class ModelProfiler. The developer is free to define how the mapping looks like. It's possible to ignore some associations by removing them from the configuration profile. Before a mapping is started, the mapper system checks whether a profile exists for the specific case and thus completes the process.

## Properties Converter

The properties converters are provided by the ConverterProvider. Each converter implements the ITypeConverter interface. The MapHandler takes advantage of it by converting properties during the mapping process.

## Model Validation

The data or models are validated using [Fluentvalidation](https://github.com/FluentValidation/FluentValidation) Framework. For more informations, read the related documentation.

### Validation sample code

In the case of the Room model:
```c#
public class RoomValidator : MirrorModelValidator<Room>
{
    public RoomValidator() : base()
    {
        RuleFor(r => r.Category).NotNull().NotEmpty();
        RuleFor(r => r.NumberOfBad).GreaterThanOrEqualTo(0);
        RuleFor(r => r.NumberOfBed).GreaterThanOrEqualTo(1);
    }
}
```

## Logging

The looging is managed by the Logger Component of the [NLog](https://github.com/NLog/NLog)  nuget package. A configuration file 'NLog.config' must be added in the module or project marked as Start-up project. The Logger class is injected as an ILogger interface in the components or classes that need to log some exceptions or warnings.

### Nlog.config sample

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xmlns="http://www.nlog-project.org/schemas/NLog.xsd">

    <targets>
        <target name="logfile" xsi:type="File" fileName="Logs/log.txt"/>
        <target name="logconsole" xsi:type="Console"/>
    </targets>

    <rules>
        <logger name="*" minlevel="Info" writeTo="logconsole"/>
        <logger name="*" minlevel="Info" writeTo="logfile"/>
    </rules>
</nlog>
```


# How to use the mapper system

1. Add new model for the internal and external system
   - Add model and the corresponding validator
  ```c#
    public class NewModel: MirrorModel
    {
        public NewModel():base(new NewModelValidator())
        {
            
        }
        public int ClientId { get; set; }
        public int NumberOfUsers { get; set; }
    }

  ```
2. Create the associated profile for the mapping in the ModelMapper.Core component
   
  ```c#  
    public class NewModelProfile : BaseProfile<sourceNamespace.NewModel, targetNamespace.NewModel>
    {
        public NewModelProfile() : base()
        {
        }
        protected override void BuildProfile()
        {
            InsertPropetiesMapping(nameof(targetNamespace.NewModel.ClientId).ToLowerInvariant(), nameof(sourceNamespace.NewModel.ClientId).ToLowerInvariant());...
        }
    }

  ```


3. Add a type converter
  If you want a property type to be converted correctly, create a new converter in the ModelMapper.Core component as follows and add it in the CreateFilters method of the ConverterProvider class.

  ```c#
    public class GuidConverter: ITypeConverter
    {
        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public Type TargetType => typeof(Guid);

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <returns>The convertion method</returns>
        public Func<object, object> GetMethod()
        {
            Func<object, object> func = (input) =>
            {
                //implement the convertion...
                return default;
            };

            return func;
        }
    }

        //Add your new converter 
    private void CreateFilters()
    {
        //Add the default converter at first converter. It will be used as default converter
        ITypeConverter converter = new DefaultConverter();
        _converters.Add(converter.TargetType, converter);

        //Add the guid converter
        converter = new GuidConverter();
        _converters.Add(converter.TargetType, converter);

        //--> add the new converter hier...

    }

  ```
   
4. Add a payload handler

If the payload object is not already supported, please create a new one like the JSONPlayoadHandler as a static class and add the new case in the MapPayload method of the MapHandler class

5. Test your code
   ```c#
    IModelProfiler modelProfiler = new ModelProfiler();
    IConverterProvider converterProvider = new ConverterProvider();
    ILogger logger = LogManager.GetCurrentClassLogger();

    IMapHandler mapHandler = new MapHandler(modelProfiler, converterProvider, logger);

    //1. Map the room model
     DIRS21.DataModel.Room room = new DIRS21.DataModel.Room() { Id = "1587-1457-p90", NumberOfBad = 1, NumberOfBed = 3, Category = "family", Details = "With animals themes..." };
    Console.WriteLine($"Source:\n{JsonConvert.SerializeObject(room)}");

    object targetRoomModel = mapHandler.Map(room, typeof(DIRS21.DataModel.Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!);
    Console.WriteLine($"\nTarget:\n{JsonConvert.SerializeObject(targetRoomModel)}");

   ```

   # Limitations of the mapper system
   
   The mapper system supports the mapping of the Collections that implement the interface [IList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ilist?view=net-8.0). To support additional interfaces or another collection, the MapCollection method of the MaHandler should be extended to achieve this goal.




