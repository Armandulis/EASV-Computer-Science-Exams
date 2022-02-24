<?php

/**
 * Testing in PHP: unit testing, frameworks, mocking, and data-driven tests
 */

namespace App\Tests;

use App\Calculator\CalculatorController;
use App\Models\ScientificCalculator;
use PHPUnit\Framework\MockObject\MockObject;
use PHPUnit\Framework\TestCase;
use Symfony\Component\Filesystem\Filesystem;

/**
 * Class CalculatorControllerTest
 * @package App\Tests
 * @covers \App\Calculator\CalculatorController
 */
class CalculatorControllerTest extends TestCase
{
    /** @var Filesystem&MockObject */
    private $FileSystem;

    /** @var ScientificCalculator&MockObject */
    private $ScientificCalculator;

    /** @var CalculatorController&MockObject */
    private $CalculatorController;

    /**
     * Runs before every test
     */
    protected function setUp(): void
    {
        parent::setUp();

        // Set up mocks
        $this->FileSystem = $this->getMockBuilder(Filesystem::class)
            ->onlyMethods(['appendToFile', 'exists'])
            ->getMock();
        $this->ScientificCalculator = $this->getMockBuilder(ScientificCalculator::class)->getMock();

        // Set up the controller we're testing
        $this->CalculatorController = $this->getMockBuilder(CalculatorController::class)
            // Pass constructor arguments
            ->setConstructorArgs([$this->ScientificCalculator, $this->FileSystem])
            // Mock specific methods
            ->onlyMethods(['getScientificCalculatorSavedConstants'])
            ->getMock();
    }

    public function test_divideInput_InputTwoIsZero_ThrowsException(): void
    {
        // Given input variables
        $inputOne = 1;
        $inputTwo = 0;

        // Then we expect exception with this message
        $this->expectException(\Exception::class);
        $this->expectExceptionMessage('Cannot divide from 0');

        // When we call divideInput
        $this->CalculatorController->divideInput($inputOne, $inputTwo);
    }

    public function test_divideInput_normalInput_SavesResultToCalculatorAnswersFile(): void
    {
        // Given input variables
        $inputOne = 10;
        $inputTwo = 2;

        // Given file names 'calculator-answers' exists
        $this->FileSystem->method('exists')->willReturn(true);

        // Then we expect to call appendToFile with calculation result once
        $this->FileSystem->expects( self::once() )->method('appendToFile')->with('calculator-answers', 5);

        // When we call divideInput
        $result = $this->CalculatorController->divideInput($inputOne, $inputTwo);

        // Then we expect the result to be 2
        self::assertSame(5, $result);
    }

    public function test_divideInput_normalInput_SavesResultToAnswersFile(): void
    {
        // Given input variables
        $inputOne = 15;
        $inputTwo = 5;

        // Given file names 'calculator-answers' does not exist
        $this->FileSystem->method('exists')->willReturn(false);

        // Then we expect to call appendToFile with calculation result once
        $this->FileSystem->expects( self::once() )->method('appendToFile')->with('answers', 3);

        // When we call divideInput
        $result = $this->CalculatorController->divideInput($inputOne, $inputTwo);

        // Then we expect the result to be 2
        self::assertSame(3, $result);
    }

    /**
     * @dataProvider mediumResultSaveDataProvider
     * @param int $inputOne
     * @param int $inputTwo
     */
    public function test_saveMediumResultSubject_IsTooLowOrHigh_returnsFalse(int $inputOne, int $inputTwo): void
    {
        // Given subject
        $subject = 'Building-21A';

        // Given saveSubject returns true if called
        $this->ScientificCalculator->method('saveSubject')->willReturn(true);

        // When we call saveMediumResultSubject
        $result = $this->CalculatorController->saveMediumResultSubject($subject, $inputOne, $inputTwo);

        // Then we expect the result to be false
        self::assertFalse($result);
    }

    /**
     * Returns strict edge cased values needed for test_saveMediumResultSubject_IsTooLowOrHigh_returnsFalse
     * @return array<string, int[]>
     */
    public function mediumResultSaveDataProvider(): array
    {
        return [
            'Both inputs are too low' => [1, 2],
            'Both inputs are one off' => [3, 3],
            'Both inputs are one off too high' => [11, 11],
            'Both inputs are too high' => [14, 13],
        ];
    }

    public function test_saveMediumResultSubject_normalInput_returnsTrue(): void
    {
        // Given subject
        $subject = 'Building-21A';
        $inputOne = 8;
        $inputTwo = 6;

        // Given saveSubject returns true if called
        $this->ScientificCalculator->method('saveSubject')->willReturn(true);

        // When we call saveMediumResultSubject
        $result = $this->CalculatorController->saveMediumResultSubject($subject, $inputOne, $inputTwo);

        // Then we expect the result to be true
        self::assertTrue($result);
    }

    public function test_CalculateMultiplicationWithConstants_hasConstants_doesCalculations(): void
    {
        // Given subject and input
        $subject = 'Width of tires';
        $input = 4;

        // When we call getScientificCalculatorSavedConstants with constants
        $constants = [12, 26, 87];
        $this->CalculatorController->method('getScientificCalculatorSavedConstants')
            ->willReturn($constants)
            ->with($subject);


        // When we call calculateMultiplicationWithConstants
        $result = $this->CalculatorController->calculateMultiplicationWithConstants($subject, $input);

        // Then we expect the result to be input multiplied by constants
        self::assertSame($input * 12 * 26 * 87, $result);
    }

    public function test_saveMultiplicationSequence_normalInput_returnsArray(): void
    {
        // Given number, subject and multiplications
        $subject = '';
        $number = 4;
        $multiplications = [4, 2, 6];

        // Then we expect to call saveSubject 3 times with following arguments
        $this->ScientificCalculator->expects( self::exactly( 3 ))->method( 'saveSubject' )
            ->withConsecutive(
                [ $subject, 1*4 ],
                [ $subject, 1*4*2],
                [ $subject, 1*4*2*6]
            );

        // When we call saveMultiplicationSequence
        $result = $this->CalculatorController->saveMultiplicationSequence($subject, $number, $multiplications);

        // Then we expect the result to be size of 3 (because out multiplications size if 3)
        self::assertCount( 3, $result);
    }

    public function test_savesSquareResultAsJson_ThrowsException_returnsNull() : void
    {
        // Given subject and number
        $subject = 'json';
        $number = 5;

        // When exception occurs when calling 'saveSubjectJson'
        $this->ScientificCalculator->method( 'saveSubjectJson' )->willThrowException( new \Exception() );

        // When we call savesSquareResultAsJson
        $result = $this->CalculatorController->savesSquareResultAsJson( $subject, $number );

        // Then we except the result to be false
        self::assertNull($result);
    }

    public function test_SavesSquareResultAsJson_normalInput_returnsJsonString() : void
    {
        // Given subject and number
        $subject = 'json';
        $number = 5;

        // Then we expect to call 'saveSubjectJson' once with subject and any other value
        $this->ScientificCalculator->expects( self::once())->method( 'saveSubjectJson' )->with($subject, self::anything());

        // When we call savesSquareResultAsJson
        $result = $this->CalculatorController->savesSquareResultAsJson( $subject, $number );

        // Then we except the result to be false
        self::assertJsonStringEqualsJsonString( '{"result": "25"}', $result);
    }
}
